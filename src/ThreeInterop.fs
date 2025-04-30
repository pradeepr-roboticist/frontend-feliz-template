module ThreeInterop


open Fable.Core
open Fable.Core.JsInterop

type Geometry = obj // replace with proper types if needed
type MaterialOptions = obj

[<Import("initScene", from="./js/threeInterop.js")>]
let initScene (id: string) (containerId: string) (callbacks: unit -> unit) : unit = jsNative

[<Import("callSceneFunction", from="./js/threeInterop.js")>]
let callSceneFunction (id: string) (functionName: string) (args: obj[]) : unit = jsNative

[<Import("updateCallbacks", from="./js/threeInterop.js")>]
let updateCallbacks (id: string) (newCallbacks: obj) : unit = jsNative

module Helpers =
    let onRender (f: unit -> unit) = "onRender" ==> f
    let onResize (f: unit -> unit) = "onResize" ==> f
