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
	}
}
