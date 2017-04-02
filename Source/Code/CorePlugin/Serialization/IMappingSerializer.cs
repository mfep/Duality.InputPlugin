namespace MFEP.Duality.Plugins.InputPlugin.Serialization
{
	public interface IMappingSerializer
	{
		object LoadMapping ();
		void SaveMapping (object obj);
	}
}