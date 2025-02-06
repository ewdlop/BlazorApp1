//// Hooks.fs (Example hooks)
namespace ClassLibrary1

//open Microsoft.AspNetCore.Components
//open Microsoft.FSharp.Reflection

//module Hooks =
//    /// useState 透過 Hook.create 建立，型別為：'T -> (unit -> unit) -> Hook<'T>
//    let useState<'T> : 'T -> (unit -> unit) -> Hook<'T> =
//        Hook.create<'T> ()
    
//    /// useCounter 回傳當前計數值與增、減、重設函數
//    let useCounter initialValue (triggerStateHasChanged: unit -> unit) =
//        let state = useState initialValue triggerStateHasChanged
//        let increment () = state.Update(fun x -> x + 1)
//        let decrement () = state.Update(fun x -> x - 1)
//        let reset () = state.Update(fun _ -> initialValue)
//        state.Value, increment, decrement, reset
    
//    /// useToggle 用於布林值切換
//    let useToggle initialValue (triggerStateHasChanged: unit -> unit) =
//        let state = useState initialValue triggerStateHasChanged
//        let toggle () = state.Update(not)
//        state.Value, toggle
    
//    /// useForm 用於更新記錄型態中指定欄位的值
//    let useForm<'T> (initial: 'T) (triggerStateHasChanged: unit -> unit) =
//        let state = useState initial triggerStateHasChanged
//        let update field value =
//            state.Update(fun form ->
//                let props = FSharpType.GetRecordFields(typeof<'T>)
//                let values = FSharpValue.GetRecordFields(form)
//                let newValues =
//                    values |> Array.mapi (fun i v -> if props.[i].Name = field then value else v)
//                FSharpValue.MakeRecord(typeof<'T>, newValues) :?> 'T)
//        state.Value, update
