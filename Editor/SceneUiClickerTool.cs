using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

namespace Vverum.Tools.SceneUiClicker
{
	[EditorTool("Ui clicker Tool")]
	public class SceneUiClickerTool : EditorTool
	{
		private Texture iconTexture;

		public override GUIContent toolbarIcon
		{
			get
			{
				return new GUIContent()
				{
					image = iconTexture,
					text = "Use ui in Sceneview",
					tooltip = "Use game ui in editor scene view."
				};
			}
		}

		private void OnEnable()
		{
			iconTexture = AssetDatabase.LoadAssetAtPath<Texture>($"{SceneUiClickerConstants.PACKAGE_PATH}/Icons/SceneUiClicker_Icon_white.png");
#if UNITY_2020_2_OR_NEWER
			ToolManager.activeToolChanged += ActiveToolDidChange;
#else
			EditorTools.activeToolChanged += ActiveToolDidChange;
#endif
		}

		private void OnDisable()
		{
#if UNITY_2020_2_OR_NEWER
			ToolManager.activeToolChanged -= ActiveToolDidChange;
#else
			EditorTools.activeToolChanged -= ActiveToolDidChange;
#endif
		}

		public override bool IsAvailable()
		{
			return Application.isEditor && Application.isPlaying;
		}

		public override void OnToolGUI(EditorWindow window)
		{
			var e = Event.current;
			if (e.type == EventType.Repaint)
			{
				return;
			}
			else if (e.type == EventType.Layout)
			{
				HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
			}
			else
			{
				if ( e.button == 0 && e.modifiers == EventModifiers.None)
				{
					HandleInput(e);
					e.Use();
				}
			}
		}

		private void ActiveToolDidChange()
		{
#if UNITY_2020_2_OR_NEWER
			if (ToolManager.IsActiveTool(this))
#else
			if (EditorTools.IsActiveTool(this))
#endif
			{
				//todo find ui event handler
				//BaseInputModule or EventSystem
				return;
			}
		}

		private void HandleInput(Event e)
		{
			var camera = Camera.current;
			if (camera == null)
			{
				return;
			}
			//todo: handle drag up and down

			switch (e.type)
			{
				case EventType.MouseDown:
				case EventType.TouchDown:
					break;
				case EventType.MouseUp:
				case EventType.TouchUp:
					break;
				case EventType.MouseMove:
				case EventType.TouchMove:
				case EventType.MouseDrag:
					break;
				case EventType.DragUpdated:
					break;
				case EventType.DragPerform:
					break;
				case EventType.DragExited:
					break;
				case EventType.MouseEnterWindow:
					break;
				case EventType.MouseLeaveWindow:
					break;
				case EventType.TouchEnter:
					break;
				case EventType.TouchLeave:
					break;
				case EventType.TouchStationary:
				default:
					break;
			}


			var ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
		}

	}
}