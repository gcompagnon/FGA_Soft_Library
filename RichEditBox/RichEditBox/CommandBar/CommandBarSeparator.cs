// ---------------------------------------------------------
// Windows Forms CommandBar Control
// Copyright (C) 2001-2003 Lutz Roeder. All rights reserved.
// http://www.aisto.com/roeder
// roeder@aisto.com
// ---------------------------------------------------------
namespace vbMaf.Windows.Forms.CommandBar
{
	using System.ComponentModel;

	[DesignTimeVisible(false), ToolboxItem(false)]
	public class CommandBarSeparator : CommandBarItem
	{
		public CommandBarSeparator() : base("-")
		{
		}
	}
}
