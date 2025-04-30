module App

open Fable.Core.JsInterop
open Elmish
open Elmish.React

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif
Fable.Core.JsInterop.importSideEffects "bulma/css/bulma.min.css"
Fable.Core.JsInterop.importAll "three"

Program.mkProgram Index.init Index.update Index.view
// #if DEBUG
// |> Program.withConsoleTrace
// #endif
|> Program.withReactSynchronous "elmish-app"
// #if DEBUG
// |> Program.withDebugger
// #endif
|> Program.run