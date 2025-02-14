﻿// Nu Game Engine.
// Copyright (C) Bryan Edds, 2013-2023.

namespace Nu
open System
open System.IO
open Prime

[<AutoOpen; ModuleBinding>]
module WorldGameModule =

    type Game with

        member this.GetDispatcher world = World.getGameDispatcher this world
        member this.Dispatcher = lensReadOnly (nameof this.Dispatcher) this this.GetDispatcher
        member this.GetModelGeneric<'a> world = World.getGameModel<'a> this world
        member this.SetModelGeneric<'a> value world = World.setGameModel<'a> false value this world |> snd'
        member this.ModelGeneric<'a> () = lens Constants.Engine.ModelPropertyName this this.GetModelGeneric<'a> this.SetModelGeneric<'a>
        member this.GetOmniScreenOpt world = World.getGameOmniScreenOpt this world
        member this.SetOmniScreenOpt value world = World.setGameOmniScreenOpt value this world |> snd'
        member this.OmniScreenOpt = lens (nameof this.OmniScreenOpt) this this.GetOmniScreenOpt this.SetOmniScreenOpt
        member this.GetSelectedScreenOpt world = World.getGameSelectedScreenOpt this world
        member this.SelectedScreenOpt = lensReadOnly (nameof this.SelectedScreenOpt) this this.GetSelectedScreenOpt
        member this.GetDesiredScreen world = World.getGameDesiredScreen this world
        member this.SetDesiredScreen value world = World.setGameDesiredScreen value this world |> snd'
        member this.DesiredScreen = lens (nameof this.DesiredScreen) this this.GetDesiredScreen this.SetDesiredScreen
        member this.GetScreenTransitionDestinationOpt world = World.getGameScreenTransitionDestinationOpt this world
        member this.SetScreenTransitionDestinationOpt value world = World.setGameScreenTransitionDestinationOpt value this world |> snd'
        member this.ScreenTransitionDestinationOpt = lens (nameof this.ScreenTransitionDestinationOpt) this this.GetScreenTransitionDestinationOpt this.SetScreenTransitionDestinationOpt
        member this.GetEyeCenter2d world = World.getGameEyeCenter2d this world
        member this.SetEyeCenter2d value world = World.setGameEyeCenter2d value this world |> snd'
        member this.EyeCenter2d = lens (nameof this.EyeCenter2d) this this.GetEyeCenter2d this.SetEyeCenter2d
        member this.GetEyeSize2d world = World.getGameEyeSize2d this world
        member this.SetEyeSize2d value world = World.setGameEyeSize2d value this world |> snd'
        member this.EyeSize2d = lens (nameof this.EyeSize2d) this this.GetEyeSize2d this.SetEyeSize2d
        member this.GetEyeCenter3d world = World.getGameEyeCenter3d this world
        member this.SetEyeCenter3d value world = World.setGameEyeCenter3d value this world |> snd'
        member this.EyeCenter3d = lens (nameof this.EyeCenter3d) this this.GetEyeCenter3d this.SetEyeCenter3d
        member this.GetEyeRotation3d world = World.getGameEyeRotation3d this world
        member this.SetEyeRotation3d value world = World.setGameEyeRotation3d value this world |> snd'
        member this.EyeRotation3d = lens (nameof this.EyeRotation3d) this this.GetEyeRotation3d this.SetEyeRotation3d
        member this.GetScriptFrame world = World.getGameScriptFrame this world
        member this.ScriptFrame = lensReadOnly (nameof this.ScriptFrame) this this.GetScriptFrame
        member this.GetOrder world = World.getGameOrder this world
        member this.Order = lensReadOnly (nameof this.Order) this this.GetOrder
        member this.GetId world = World.getGameId this world
        member this.Id = lensReadOnly (nameof this.Id) this this.GetId

        member this.RegisterEvent = Events.RegisterEvent --> Game.Handle
        member this.UnregisteringEvent = Events.UnregisteringEvent --> Game.Handle
        member this.ChangeEvent propertyName = Events.ChangeEvent propertyName --> Game.Handle
        member this.LifeCycleEvent simulantTypeName = Events.LifeCycleEvent simulantTypeName --> Game.Handle
        member this.IntegrationEvent = Events.IntegrationEvent --> Game.Handle
        member this.PreUpdateEvent = Events.PreUpdateEvent --> Game.Handle
        member this.UpdateEvent = Events.UpdateEvent --> Game.Handle
        member this.PostUpdateEvent = Events.PostUpdateEvent --> Game.Handle
        member this.RenderEvent = Events.RenderEvent --> Game.Handle
        member this.MouseMoveEvent = Events.MouseMoveEvent --> Game.Handle
        member this.MouseDragEvent = Events.MouseDragEvent --> Game.Handle
        member this.MouseLeftChangeEvent = Events.MouseLeftChangeEvent --> Game.Handle
        member this.MouseLeftDownEvent = Events.MouseLeftDownEvent --> Game.Handle
        member this.MouseLeftUpEvent = Events.MouseLeftUpEvent --> Game.Handle
        member this.MouseMiddleChangeEvent = Events.MouseMiddleChangeEvent --> Game.Handle
        member this.MouseMiddleDownEvent = Events.MouseMiddleDownEvent --> Game.Handle
        member this.MouseMiddleUpEvent = Events.MouseMiddleUpEvent --> Game.Handle
        member this.MouseRightChangeEvent = Events.MouseRightChangeEvent --> Game.Handle
        member this.MouseRightDownEvent = Events.MouseRightDownEvent --> Game.Handle
        member this.MouseRightUpEvent = Events.MouseRightUpEvent --> Game.Handle
        member this.MouseX1ChangeEvent = Events.MouseX1ChangeEvent --> Game.Handle
        member this.MouseX1DownEvent = Events.MouseX1DownEvent --> Game.Handle
        member this.MouseX1UpEvent = Events.MouseX1UpEvent --> Game.Handle
        member this.MouseX2ChangeEvent = Events.MouseX2ChangeEvent --> Game.Handle
        member this.MouseX2DownEvent = Events.MouseX2DownEvent --> Game.Handle
        member this.MouseX2UpEvent = Events.MouseX2UpEvent --> Game.Handle
        member this.KeyboardKeyChangeEvent = Events.KeyboardKeyChangeEvent --> Game.Handle
        member this.KeyboardKeyDownEvent = Events.KeyboardKeyDownEvent --> Game.Handle
        member this.KeyboardKeyUpEvent = Events.KeyboardKeyUpEvent --> Game.Handle
        member this.GamepadDirectionChangeEvent index = Events.GamepadDirectionChangeEvent index --> Game.Handle
        member this.GamepadButtonChangeEvent index = Events.GamepadButtonChangeEvent index --> Game.Handle
        member this.GamepadButtonDownEvent index = Events.GamepadButtonDownEvent index --> Game.Handle
        member this.GamepadButtonUpEvent index = Events.GamepadButtonUpEvent index --> Game.Handle
        member this.AssetsReloadEvent = Events.AssetsReloadEvent --> Game.Handle
        member this.BodyAddingEvent = Events.BodyAddingEvent --> Game.Handle
        member this.BodyRemovingEvent = Events.BodyRemovingEvent --> Game.Handle
        member this.BodySeparationImplicitEvent = Events.BodySeparationImplicitEvent --> Game.Handle

        /// Try to get a property value and type.
        member this.TryGetProperty propertyName world =
            let mutable property = Unchecked.defaultof<_>
            let found = World.tryGetGameProperty (propertyName, this, world, &property)
            if found then Some property else None

        /// Get a property value and type.
        member this.GetProperty propertyName world =
            World.getGameProperty propertyName this world

        /// Get an xtension property value.
        member this.TryGet<'a> propertyName world : 'a =
            World.tryGetGameXtensionValue<'a> propertyName this world

        /// Get an xtension property value.
        member this.Get<'a> propertyName world : 'a =
            World.getGameXtensionValue<'a> propertyName this world

        /// Try to set a property value with explicit type.
        member this.TrySetProperty propertyName property world =
            World.trySetGameProperty propertyName property this world

        /// Set a property value with explicit type.
        member this.SetProperty propertyName property world =
            World.setGameProperty propertyName property this world |> snd'

        /// To try set an xtension property value.
        member this.TrySet<'a> propertyName (value : 'a) world =
            let property = { PropertyType = typeof<'a>; PropertyValue = value }
            World.trySetGameXtensionProperty propertyName property this world

        /// Set an xtension property value.
        member this.Set<'a> propertyName (value : 'a) world =
            let property = { PropertyType = typeof<'a>; PropertyValue = value }
            World.setGameXtensionProperty propertyName property this world

        /// Check that a game dispatches in the same manner as the dispatcher with the given type.
        member this.Is (dispatcherType, world) = Reflection.dispatchesAs dispatcherType (this.GetDispatcher world)

        /// Check that a game dispatches in the same manner as the dispatcher with the given type.
        member this.Is<'a> world = this.Is (typeof<'a>, world)

        /// Get a game's change event address.
        member this.GetChangeEvent propertyName = Game.Handle.ChangeEvent propertyName

        /// Send a signal to a game.
        member this.Signal<'message, 'command> (signal : Signal) world =
            (this.GetDispatcher world).Signal (signal, this, world)

    type World with

        static member internal registerGame (game : Game) world =
            let dispatcher = game.GetDispatcher world
            let world = dispatcher.Register (game, world)
            let eventTrace = EventTrace.debug "World" "registerGame" "Register" EventTrace.empty
            let world = World.publishPlus () game.RegisterEvent eventTrace game true false world
            let eventTrace = EventTrace.debug "World" "registerGame" "LifeCycle" EventTrace.empty
            World.publishPlus (RegisterData game) (game.LifeCycleEvent (nameof Game)) eventTrace game true false world

        static member internal unregisterGame (game : Game) world =
            let dispatcher = game.GetDispatcher world
            let eventTrace = EventTrace.debug "World" "registerGame" "LifeCycle" EventTrace.empty
            let world = World.publishPlus () game.UnregisteringEvent eventTrace game true false world
            let eventTrace = EventTrace.debug "World" "unregisteringGame" "" EventTrace.empty
            let world = World.publishPlus (UnregisteringData game) (game.LifeCycleEvent (nameof Game)) eventTrace game true false world
            dispatcher.Unregister (game, world)

        static member internal preUpdateGame (game : Game) world =
                
            // pre-update via dispatcher
            let dispatcher = game.GetDispatcher world
            let world = dispatcher.PreUpdate (game, world)

            // publish pre-update event
            let eventTrace = EventTrace.debug "World" "preUpdateGame" "" EventTrace.empty
            World.publishPlus () game.PreUpdateEvent eventTrace game false false world

        static member internal updateGame (game : Game) world =

            // update via dispatcher
            let dispatcher = game.GetDispatcher world
            let world = dispatcher.Update (game, world)

            // publish update event
            let eventTrace = EventTrace.debug "World" "updateGame" "" EventTrace.empty
            World.publishPlus () game.UpdateEvent eventTrace game false false world

        static member internal postUpdateGame (game : Game) world =
                
            // post-update via dispatcher
            let dispatcher = game.GetDispatcher world
            let world = dispatcher.PostUpdate (game, world)

            // publish post-update event
            let eventTrace = EventTrace.debug "World" "postUpdateGame" "" EventTrace.empty
            World.publishPlus () game.PostUpdateEvent eventTrace game false false world

        static member internal renderGame (game : Game) world =

            // render via dispatcher
            let dispatcher = game.GetDispatcher world
            let world = dispatcher.Render (game, world)

            // publish render event
            let eventTrace = EventTrace.debug "World" "renderGame" "" EventTrace.empty
            World.publishPlus () game.RenderEvent eventTrace game false false world

        /// Edit a game with the given operation using the ImGui APIs.
        /// Intended only to be called by editors like Gaia.
        static member editGame operation (game : Game) world =
            let dispatcher = game.GetDispatcher world
            dispatcher.Edit (operation, game, world)

        /// Get all the entities in the world.
        [<FunctionBinding "getEntitiesFlattened0">]
        static member getEntitiesFlattened1 world =
            World.getGroups1 world |>
            Seq.map (fun group -> World.getEntitiesFlattened group world) |>
            Seq.concat

        /// Get all the groups in the world.
        [<FunctionBinding "getGroups0">]
        static member getGroups1 world =
            World.getScreens world |>
            Seq.map (fun screen -> World.getGroups screen world) |>
            Seq.concat

        /// Write a game to a game descriptor.
        static member writeGame gameDescriptor game world =
            let gameState = World.getGameState game world
            let gameDispatcherName = getTypeName gameState.Dispatcher
            let gameDescriptor = { gameDescriptor with GameDispatcherName = gameDispatcherName }
            let gameProperties = Reflection.writePropertiesFromTarget tautology3 gameDescriptor.GameProperties gameState
            let gameDescriptor = { gameDescriptor with GameProperties = gameProperties }
            let screens = World.getScreens world
            { gameDescriptor with ScreenDescriptors = World.writeScreens screens world }

        /// Write a game to a file.
        [<FunctionBinding>]
        static member writeGameToFile (filePath : string) game world =
            let filePathTmp = filePath + ".tmp"
            let prettyPrinter = (SyntaxAttribute.defaultValue typeof<GameDescriptor>).PrettyPrinter
            let gameDescriptor = World.writeGame GameDescriptor.empty game world
            let gameDescriptorStr = scstring gameDescriptor
            let gameDescriptorPretty = PrettyPrinter.prettyPrint gameDescriptorStr prettyPrinter
            File.WriteAllText (filePathTmp, gameDescriptorPretty)
            File.Delete filePath
            File.Move (filePathTmp, filePath)

        /// Read a game from a game descriptor.
        static member readGame gameDescriptor name world =

            // make the dispatcher
            let dispatcherName = gameDescriptor.GameDispatcherName
            let dispatchers = World.getGameDispatchers world
            let dispatcher =
                match Map.tryFind dispatcherName dispatchers with
                | Some dispatcher -> dispatcher
                | None -> failwith ("Could not find a GameDispatcher named '" + dispatcherName + "'.")

            // make the game state and populate its properties
            let gameState = GameState.make dispatcher
            let gameState = Reflection.attachProperties GameState.copy gameState.Dispatcher gameState world
            let gameState = Reflection.readPropertiesToTarget GameState.copy gameDescriptor.GameProperties gameState

            // set the game's state in the world
            let game = Game name
            let world = World.setGameState gameState game world

            // read the game's screens
            let world = World.readScreens gameDescriptor.ScreenDescriptors world |> snd
            (game, world)

        /// Read a game from a file.
        [<FunctionBinding>]
        static member readGameFromFile (filePath : string) world =
            let gameDescriptorStr = File.ReadAllText filePath
            let gameDescriptor = scvalue<GameDescriptor> gameDescriptorStr
            World.readGame gameDescriptor world