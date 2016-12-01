namespace MFEP.Duality.Plugins.InputPlugin
{
	public interface IMappingSerializer
	{
		object LoadMapping ();
		void SaveMapping (object obj);
	}
}