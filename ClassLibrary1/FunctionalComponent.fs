// FunctionalComponent.fs
namespace ClassLibrary1

open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Rendering

// Type aliases for clarity
type ComponentId = string
type Props = Map<string, obj>
type HtmlAttributes = Map<string, obj>
type EventCallback<'T> = 'T -> unit
type RenderFunction = RenderTreeBuilder -> int -> unit

// Discriminated union for virtual DOM representation
type VNode =
    | Element of name: string * attributes: HtmlAttributes * children: VNode list
    | Component of render: (Props -> VNode)
    | Text of string
    | Fragment of VNode list
    | Empty

// Component functions module
module Component =
    let createElement name (attributes: HtmlAttributes) (children: VNode list) =
        Element(name, attributes, children)
        
    let createComponent (render: Props -> VNode) =
        Component render
        
    let text content = Text content
    
    let fragment children = Fragment children
    
    let empty = Empty
    
    // Helper to create HTML attributes map
    let attrs kvs = Map.ofList kvs
    
    // Helper to combine VNodes
    let combine nodes = Fragment nodes

open Component

// Renderer module
module Renderer =
    let rec renderVNode (builder: RenderTreeBuilder) sequence node =
        match node with
        | Element(name, attributes, children) ->
            builder.OpenElement(sequence, name)
            
            // Add attributes
            attributes 
            |> Map.iter (fun key value ->
                builder.AddAttribute(sequence + 1, key, value))
                
            // Render children
            children 
            |> List.iteri (fun i child -> 
                renderVNode builder (sequence + 2 + i) child)
                
            builder.CloseElement()
            
        | Component render ->
            let vnode = render Map.empty
            renderVNode builder sequence vnode
            
        | Text content ->
            builder.AddContent(sequence, content)
            
        | Fragment children ->
            children 
            |> List.iteri (fun i child -> 
                renderVNode builder (sequence + i) child)
                
        | Empty -> ()

// Example functional components
module Components =
    let Button (props: Props) =
        let text = props.["text"] :?> string
        let onClick = props.["onClick"] :?> (unit -> unit)
        
        Element(
            "button",
            attrs [
                "class", "btn"
                "onclick", box onClick
            ],
            [Text text]
        )
        
    let Input (props: Props) =
        let value = props.["value"] :?> string
        let onChange = props.["onChange"] :?> (string -> unit)
        
        Element(
            "input",
            attrs [
                "value", value
                "onchange", box (fun (e: ChangeEventArgs) -> 
                    e.Value :?> string |> onChange)
            ],
            []
        )
        
    let Card (props: Props) =
        let title = props.["title"] :?> string
        let content = props.["content"] :?> VNode list
        
        Element(
            "div",
            attrs ["class", "card"],
            [
                Element(
                    "div",
                    attrs ["class", "card-header"],
                    [Text title]
                )
                Element(
                    "div",
                    attrs ["class", "card-body"],
                    content
                )
            ]
        )
open Components
open System
open Microsoft.AspNetCore.Components.Web

// Example usage in a Blazor component
type FunctionalCounterComponent() =
    inherit ComponentBase()
    
    let mutable count = 0
    
    let increment () = count <- count + 1
    
    let counterView () =
        Element(
            "div",
            attrs [],
            [
                Text $"Count: {count}"
                Button (
                    attrs [
                        "text", "Increment"
                        "onClick", box (fun () -> increment())
                    ]
                )
            ]
        )
    
    override this.BuildRenderTree(builder) =
        Renderer.renderVNode builder 0 (counterView())
        
// Composable form example
module Form =
    type FormField =
        { Label: string
          Value: string
          OnChange: string -> unit }
          
    let FormInput (field: FormField) =
        Element(
            "div",
            attrs ["class", "form-group"],
            [
                Element(
                    "label",
                    attrs [],
                    [Text field.Label]
                )
                Input (
                    attrs [
                        "value", field.Value
                        "onChange", field.OnChange
                    ]
                )
            ]
        )
        
    let Form fields =
        Element(
            "form",
            attrs [],
            fields |> List.map FormInput
        )

type Compoent() =
    inherit ComponentBase()
    member _.StateHasChanged() = base.StateHasChanged()

// Hooks-like state management
module State =
    type StateHook<'T> = {
        Value: 'T
        SetValue: ('T -> 'T) -> unit
    }
    
    let useState<'T> (initial: 'T) (s: Compoent) =
        let mutable state = initial
        
        let setState f =
            state <- f state
            s.StateHasChanged()
            //comp.GetType().InvokeMember("StateHasChanged", System.Reflection.BindingFlags.InvokeMethod, null, comp, [||]) |> ignore
        { Value = state; SetValue = setState }
        
    // Example usage
    let Counter(comp: Compoent) =
        let state = useState 0 comp
        
        Element(
            "div",
            attrs [],
            [
                Text $"Count: {state.Value}"
                Button (
                    attrs [
                        "text", "Increment"
                        "onClick", box (fun () ->
                            state.SetValue (fun c -> c + 1))
                    ]
                )
            ]
        )
        
type TodoApp() =
    inherit ComponentBase()
    
    let mutable todos = []
    let mutable newTodo = ""
    
    let addTodo () =
        if not (String.IsNullOrEmpty newTodo) then
            todos <- newTodo :: todos
            newTodo <- ""
            
    let todoList () =
        Element(
            "div",
            attrs [],
            [
                Input (
                    attrs [
                        "value", newTodo
                        "onChange", box (fun (v: ChangeEventArgs) -> newTodo <- v.Value :?> string)
                    ]
                )
                Button (
                    attrs [
                        "text", "Add Todo"
                        "onClick", box (fun (_: MouseEventArgs) -> addTodo())
                    ]
                )
                Element(
                    "ul",
                    attrs [],
                    todos 
                    |> List.map (fun todo ->
                        Element(
                            "li",
                            attrs [],
                            [Text todo]
                        )
                    )
                )
            ]
        )
        
    override this.BuildRenderTree(builder) =
        Renderer.renderVNode builder 0 (todoList())
