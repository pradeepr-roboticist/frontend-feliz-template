module ThreeJSComponent
open Feliz

open Fable.Core.JsInterop
let view model dispatch =
    let myThreeComponent (id: string) =
        React.functionComponent(fun () ->
            let containerId = $"three-container-{id}"

            React.useEffectOnce(fun () ->
                ThreeInterop.initScene id containerId (fun () -> ())
                // ThreeInterop.initScene(
                //     id,
                //     containerId,
                //     (fun () -> ())
                // )

                // // Simulate loading a mesh after 500ms
                Browser.Dom.window.setTimeout(fun () ->
                    // let geometry = Fable.Core.JsInterop.createNew ("THREE.BoxGeometry", 1, 1, 1)
                    // let materialOpts = createObj [ "color" ==> 0xff0000 ]
                    // printfn $"Loading mesh with {geometry} and {materialOpts}"
                    ThreeInterop.callSceneFunction id "loadMesh" [|box "myBox"|]
                ,500) |> ignore

                // // Move it after 2 seconds
                // Browser.Dom.window.setTimeout((fun () ->
                //     ThreeInterop.callSceneFunction(id, "moveObject", [| box "myBox"; box 2; box 1; box 0 |])
                // ), 2000) |> ignore
            )

            Html.div [
                prop.id containerId
                prop.style [ style.width 400; style.height 300; style.border(1, borderStyle.solid, "black") ]
            ]
        )
    React.fragment [ myThreeComponent "A" () ]
