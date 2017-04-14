using System;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public class ButtonEventArgs : EventArgs
	{
		public ButtonEventArgs (string buttonName)
		{
			ButtonName = buttonName;
		}

		public string ButtonName { get; }
	}

	public class AddButtonEventArgs : ButtonEventArgs
	{
		public AddButtonEventArgs (string buttonName, ButtonTuple buttonTuple) : base (buttonName)
		{
			ButtonTuple = buttonTuple;
		}

		public ButtonTuple ButtonTuple { get; }
	}

	public class RemoveButtonEventArgs : ButtonEventArgs
	{
		public RemoveButtonEventArgs (string buttonName) : base (buttonName)
		{
		}
	}

	public class ButtonRenamedEventArgs : ButtonEventArgs
	{
		public ButtonRenamedEventArgs (string buttonName, string oldName) : base (buttonName)
		{
			OldName = oldName;
		}

		public string OldName { get; }
	}

	public class AddKeyToButtonEventArgs : ButtonEventArgs
	{
		public AddKeyToButtonEventArgs (string buttonName, KeyValue newKeyValue, KeyRole newKeyRole) : base (buttonName)
		{
			NewKeyValue = newKeyValue;
			NewKeyRole = newKeyRole;
		}

		public KeyValue NewKeyValue { get; }
		public KeyRole NewKeyRole { get; }
	}

	public class RemoveKeyFromButtonEventArgs : ButtonEventArgs
	{
		public RemoveKeyFromButtonEventArgs (string buttonName, KeyValue removedKeyValue) : base (buttonName)
		{
			RemovedKeyValue = removedKeyValue;
		}

		public KeyValue RemovedKeyValue { get; }
	}

	public class ButtonRiseTimeChangedEventArgs : ButtonEventArgs
	{
		public ButtonRiseTimeChangedEventArgs (string buttonName, float newRiseTime) : base (buttonName)
		{
			NewRiseTime = newRiseTime;
		}

		public float NewRiseTime { get; }
	}

	public class ButtonDeadZoneChangedEventArgs : ButtonEventArgs
	{
		public ButtonDeadZoneChangedEventArgs (string buttonName, float newDeadZone) : base (buttonName)
		{
			NewDeadZone = newDeadZone;
		}

		public float NewDeadZone { get; }
	}
}
