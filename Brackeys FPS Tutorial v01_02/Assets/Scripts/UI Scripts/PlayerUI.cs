using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform HealthBarFill;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    GameObject scoreBoard;

    [SerializeField]
    Text AmmoAmountText;

    private WeaponManager weaponManager;

    void SetFuelAmount(float _amount)
    {
        thrusterFuelFill.localScale = new Vector3(.88f, _amount, .88f);
    }

    void SetHealthAmount(float _amount)
    {
        HealthBarFill.localScale = new Vector3(.88f, _amount, .88f);
    }

    private Player player;
    public void SetPlayer (Player _player)
    {
        player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
    }
    private void Start()
    {
        PauseMenu.isOn = false;
        ScoreBoard.SBisOn = false;
        
    }


    void Update()
    {
        SetFuelAmount(controller.GetThrusterFuelAmount());
        SetHealthAmount(player.GetHealthBarAmt());
        SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets, weaponManager.GetCurrentWeapon().maxBullets);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("escape was pushed");
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //    scoreBoard.SetActive(true);
            //} else if (Input.GetKeyUp(KeyCode.Tab))
            //{
            //    scoreBoard.SetActive(false);
            ToggleScoreboard();
        }

    }

    public void ToggleScoreboard()
    {
        scoreBoard.SetActive(!scoreBoard.activeSelf);
        ScoreBoard.SBisOn = scoreBoard.activeSelf;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }

    void SetAmmoAmount(int _Amount, int _MaxAmount = 0)
    {
        AmmoAmountText.text = _Amount.ToString() + "/" + _MaxAmount.ToString();
    }

}

