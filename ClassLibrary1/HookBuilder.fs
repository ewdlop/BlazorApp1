//// HookBuilder.fs
namespace ClassLibrary1

///// Computation expression 用於構建 hooks 的工作流程
//type HooksBuilder() =
//    /// Bind 接收一個已部分應用的 hook（型別：unit -> Hook<'T>）
//    member _.Bind(hook: (unit -> Hook<'T>), f: Hook<'T> -> 'R) =
//        fun (triggerStateHasChanged: unit -> unit) ->
//            let h = hook triggerStateHasChanged
//            f h
//    member _.Return(x) = fun _ -> x
//    member _.ReturnFrom(x: (unit -> 'T)) = x
//    member _.Zero() = fun _ -> ()
//    member _.Combine(a: (unit -> unit), b: (unit -> 'T)) =
//        fun triggerStateHasChanged ->
//            a triggerStateHasChanged
//            b triggerStateHasChanged
//    member _.Delay(f: unit -> (unit -> 'T)) =
//        fun triggerStateHasChanged -> (f()) triggerStateHasChanged

//let hooks = HooksBuilder()
