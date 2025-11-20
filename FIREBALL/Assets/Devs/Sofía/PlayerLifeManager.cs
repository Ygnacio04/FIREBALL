using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    public int _currentLives { get; private set; }

    void Start()
    {
        _currentLives = maxLives;
    }

    //Cuando el jugador pierde una vida, llamar a esta función desde el evento/acto que causa la pérdida
    public void loseLife()
    {
        _currentLives--;
        if (_currentLives <= 0)
        {
            //PIERDES
            //TODO: Implementar que ocurre si pierdes todas las vidas
        }
    }

    //Llamar cuando se recupere vida
    /*TODO: Crear coroutine que se active cuando el numero de vidas sea menor que el máximo (IDEA: usar función loselife para evitar estar 
     "escuchando" todo el rato), llamando a esta función cada x segundos para recuperación de vida auto */ 
    public void recoverLife()
    {
        if (_currentLives < maxLives)
        {
            _currentLives++;
        }
    }
}
