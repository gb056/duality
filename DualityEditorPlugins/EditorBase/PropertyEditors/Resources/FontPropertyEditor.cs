﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Duality.Resources;

namespace Duality.Editor.Plugins.Base.PropertyEditors
{
	[PropertyEditorAssignment(typeof(Font), PropertyEditorAssignmentAttribute.PrioritySpecialized)]
	public class FontPropertyEditor : ResourcePropertyEditor
	{
		public FontPropertyEditor()
		{
			this.Indent = 0;
		}

		protected override bool IsAutoCreateMember(MemberInfo info)
		{
			return false;
		}
		protected override void BeforeAutoCreateEditors()
		{
			base.BeforeAutoCreateEditors();
			FontPreviewPropertyEditor preview = new FontPreviewPropertyEditor();
			preview.EditedType = this.EditedType;
			preview.Getter = this.GetValue;
			this.ParentGrid.ConfigureEditor(preview);
			this.AddPropertyEditor(preview);
			FontContentPropertyEditor content = new FontContentPropertyEditor();
			content.EditedType = this.EditedType;
			content.Getter = this.GetValue;
			content.Hints = HintFlags.None;
			content.HeaderHeight = 0;
			content.HeaderValueText = null;
			content.PreventFocus = true;
			content.CanRenderFontChanged += (sender, e) => 
			{
				// Switch readonly mode on an off, depending on whether we can re-render this Font dynamically.
				content.Setter = content.CanRenderFont ? this.SetValues : (Action<IEnumerable<object>>)null;
			};
			this.ParentGrid.ConfigureEditor(content);
			this.AddPropertyEditor(content);
			content.Expanded = true;
		}
	}
}
