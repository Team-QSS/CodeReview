using UnityEngine;

public class GameManager : MonoBeHaviour
{
	public GameManager instance;
	public Player _player;
	
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		
		CreatePlayer();
		Game();
	}
	
	public void CreatePlayer()
	{
		_player = Resource.Load<Player>("Player");
		_player = Instantiate(_player, new Vector3(0, 0, 0), true);
	}
	
	public void Game()
	{
		for (int i = 0; i < 3; i++)
		{
			_player.RecieveAttack(4);
			if (_player.hp <= 0)
			{
				Debug.Log("Player is dead");
				break;
			}
		}
	}
}