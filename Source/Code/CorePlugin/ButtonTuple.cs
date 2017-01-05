using Duality.Input;

namespace MFEP.Duality.Plugins.InputPlugin
{
	public class ButtonTuple
	{
		public string ButtonName { get; }
		public KeyValue[] KeyValues { get; }

		public ButtonTuple (string buttonName, KeyValue[] keyValues)
		{
			ButtonName = buttonName;
			KeyValues = keyValues;
		}

		public ButtonTuple (string buttonName, Key key)
		{
			ButtonName = buttonName;
			KeyValues = new[] { new KeyValue (key) };
		}

		public ButtonTuple (string buttonName, MouseButton mouseButton)
		{
			ButtonName = buttonName;
			KeyValues = new[] { new KeyValue (mouseButton) };
		}
	}
}
