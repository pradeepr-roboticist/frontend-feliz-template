module Page1

open Elmish

type Todo = { Id: int; Description: string }
type Model = { Todos: Todo list; Input: string }

type Msg =
    // | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    // | AddedTodo of Todo

let init () : Model * Cmd<Msg> =
    let model = { Todos = []; Input = "" }
    model, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    // | GotTodos todos -> { model with Todos = todos }, Cmd.none
    | SetInput value -> { model with Input = value }, Cmd.none
    | AddTodo ->
        let todo = { Id = model.Todos |> List.length ; Description = model.Input }
        // let cmd = Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo
        {
            model
                with
                    Input = ""
                    Todos = model.Todos @ [ todo ]
        }, Cmd.none
    // | AddedTodo todo -> { model with Todos = model.Todos @ [ todo ] }, Cmd.none

open Feliz
open Feliz.Bulma

let navBrand =
    Bulma.navbarBrand.div [
        Bulma.navbarItem.a [
            prop.href "https://safe-stack.github.io/"
            navbarItem.isActive
            prop.children [
                Html.img [
                    prop.src "/favicon-32x32.png"
                    prop.alt "Logo"
                ]
            ]
        ]
    ]

let containerBox (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            Html.ol [
                Html.div [
                    prop.style [
                        style.fontSize 30
                    ]
                    prop.children [Html.text "Todos"]
                ]
                for todo in model.Todos do
                    Html.li [ prop.text todo.Description ]
            ]
        ]
        Bulma.field.div [
            field.isGrouped
            prop.children [
                Bulma.control.p [
                    control.isExpanded
                    prop.children [
                        Bulma.input.text [
                            prop.value model.Input
                            prop.placeholder "What needs to be done?"
                            prop.onChange (fun x -> SetInput x |> dispatch)
                            prop.onKeyDown (fun e ->
                                if e.key = "Enter" then
                                    AddTodo |> dispatch)
                        ]
                    ]
                ]
            ]
        ]
    ]

let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.hero [
        hero.isFullHeight
        color.isPrimary
        // prop.style [
        //     style.backgroundSize "cover"
        //     style.backgroundImageUrl "https://unsplash.it/1200/900?random"
        //     style.backgroundPosition "no-repeat center center fixed"
        // ]
        prop.children [
            Bulma.heroHead [
                Bulma.navbar [
                    Bulma.container [ navBrand ]
                ]
            ]
            Bulma.heroBody [
                Bulma.container [
                    Bulma.column [
                        column.is6
                        column.isOffset3
                        prop.children [
                            Bulma.title [
                                text.hasTextCentered
                                prop.text "Page 1"
                            ]
                            containerBox model dispatch
                        ]
                    ]
                ]
            ]
        ]
    ]