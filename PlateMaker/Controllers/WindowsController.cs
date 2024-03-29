﻿using System.Windows.Controls;
using PlateMaker.Interfaces;
using PlateMaker.Windows;

namespace PlateMaker.Controllers
{
	internal class WindowsController
	{
		public static void ShowTextEditor(IRtfString withRtf, RichTextBox canvas)
		{
			var textEditor = new TextEditor(withRtf, canvas);
			textEditor.Show();
		}
	}
}
