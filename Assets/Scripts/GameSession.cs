public class GameSession
{
	public ShipBlueprint CurrentShipBlueprint;

	private static GameSession _instance;
	public static GameSession Instance
	{
		get
		{
			if (_instance == null)
				_instance = new GameSession();
			return _instance;
		}
	}

	private GameSession()
	{}
}