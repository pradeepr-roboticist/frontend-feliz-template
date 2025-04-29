module Index

open Elmish
open Feliz
open Feliz.Router

type Url =
    | Page1

type Page =
    | Page1 of Page1.Model
    | NotFound

type Model =
    {
        CurrentUrl : Url option
        CurrentPage : Page
    }

type Msg =
    | Page1Msg of Page1.Msg
    | UrlChanged of Url option

let tryParseUrl = function
    | [] | [ "page1" ] -> Some Url.Page1
    | _ -> None

let initPage url =
    match url with
    | Some Url.Page1 ->
        let page1Model, page1Msg = Page1.init ()
        { CurrentUrl = url; CurrentPage = (Page.Page1 page1Model) }, page1Msg |> Cmd.map Page1Msg
    | None ->
        { CurrentUrl = url; CurrentPage = Page.NotFound }, Cmd.none

let init () : Model * Cmd<Msg> =
    Router.currentPath()
    |> tryParseUrl
    |> initPage

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg, model.CurrentPage with
    | Page1Msg page1Msg, Page.Page1 page1Model  ->
        let newPage1Model, newPage1Msg = Page1.update page1Msg page1Model
        { model with CurrentPage = Page.Page1 newPage1Model }, newPage1Msg |> Cmd.map Page1Msg
    | UrlChanged urlSegments, _ ->
        initPage urlSegments
    | _ ->
        model, Cmd.none

let view (model: Model) (dispatch: Msg -> unit) =
    React.router [
        router.pathMode
        router.onUrlChanged (tryParseUrl >> UrlChanged >> dispatch)
        router.children [
            match model.CurrentPage with
            | Page.Page1 page1Model ->
                Page1.view page1Model (Page1Msg >> dispatch)
            | Page.NotFound ->
                Html.p "Not found"
        ]
    ]