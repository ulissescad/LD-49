using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform _centerOfMass;

    [SerializeField] private int _impulse;
    
    [SerializeField]private int _turningSpeed = 20;

    [SerializeField] private Animator _animator;
    
    [SerializeField] private Animator _finalMenuAnimator;

    [SerializeField] private TMP_Text _finalMenuText;
    
    [SerializeField] private Rigidbody _rdy;

    [SerializeField] private UiElement _uiElementScore;
    
    [SerializeField] private UiElement _uiElementCombo;
    
    [SerializeField] private UiElement _uiElementTime;

    [SerializeField] private Transform _uiElementScoreRoot;

    [SerializeField] private Animator _uiPause;

    [SerializeField] private bool _mainMenu = false;
    
    private int totalScore = 0;
    private int comboScore = 0;
    private bool dead = false;


    [SerializeField]private List<UiElement> _uiManuversList = new List<UiElement>();
    
    // Start is called before the first frame update
    void Start()
    {
        _rdy = GetComponent<Rigidbody>();
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {

        if (dead == true)
            return;
        
        if(_uiElementTime)
        {
        _uiElementTime.UpdateUI("Time: "+ (int)Time.time);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        //_rdy.centerOfMass = _centerOfMass.localPosition;
        
        Vector3 left = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(_centerOfMass.transform.position, left, 0.5f))
            GameOver();
        
        Vector3 right = transform.TransformDirection(-Vector3.forward);

        if (Physics.Raycast(_centerOfMass.transform.position, right, 0.5f))
            GameOver();
        
        Vector3 up = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, up, 1f))
            GameOver();

        if (Input.GetAxis("Horizontal") != 0)
        {
            _rdy.AddTorque(Input.GetAxis("Horizontal")*_turningSpeed,0,0);
        }
        
        if (Input.GetButton("Fire1"))
        {
            _animator.SetBool("superMan",true);
            
            if (Input.GetButtonDown("Fire1"))
            
            {
                CreateUI("Super Man!",50);
            }
        }
        else
        {
            _animator.SetBool("superMan",false);
        }
        
        if (Input.GetButton("Fire2"))
        {
            _animator.SetBool("kickLeft",true);

            if (Input.GetButtonDown("Fire2"))
            
            {
                CreateUI("Left Kick!",15);

            }
        }
        else
        {
            _animator.SetBool("kickLeft",false);
        }
        
        if (Input.GetButton("Fire3"))
        {
            _animator.SetBool("kickRight",true);
            
            if (Input.GetButtonDown("Fire3"))
            
            {
                
                CreateUI("Right Kick!",15);

            }
        }
        else
        {
            _animator.SetBool("kickRight",false);
        }
        
        if (Input.GetButton("Jump"))
        {
            _animator.SetBool("thinker",true);
            
            if (Input.GetButtonDown("Jump"))
            
            {
                
                CreateUI("Thinker!",30);

            }
        }
        else
        {
            _animator.SetBool("thinker",false);
        }
        

    }

    void UpdateScore(int value)
    { 
        if(_uiElementScore)
        {
            totalScore += value;
            _uiElementScore.UpdateUI("SCORE: " + totalScore);
            _uiElementScore.GetComponent<Animator>().SetTrigger("upScore");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("idle")&&dead==false)
        {

            
            if (_uiManuversList.Count > 0)
            {
                var comboUI = Instantiate(_uiElementCombo, _uiElementScoreRoot);
                
                switch (_uiManuversList.Count)
                {
                    case 2:
                        comboUI.UpdateUI("Super Combo! \n"+ _uiManuversList.Count);
                        comboScore+=30;
                        _rdy.AddForce(Vector3.up.normalized * (_impulse+3), ForceMode.Impulse);
                        break;
                    case 3:
                        comboUI.UpdateUI("Awesome Combo!! \n"+ _uiManuversList.Count);
                        comboScore+=60;
                        _rdy.AddForce(Vector3.up.normalized * (_impulse+5), ForceMode.Impulse);
                        break;
                    case 4:
                        comboUI.UpdateUI("Godlike!!! \n"+ _uiManuversList.Count);
                        comboScore+=100;
                        _rdy.AddForce(Vector3.up.normalized * (_impulse), ForceMode.Impulse);
                        break;
                    
                    default:
                        Destroy(comboUI.gameObject);
                        _rdy.AddForce(Vector3.up.normalized * (_impulse), ForceMode.Impulse);
                        break;
                        
                }
                
                StartCoroutine(Disable());
        
                IEnumerator Disable()
                {
                    yield return new WaitForSeconds(5f);
                    
                    if(comboUI!=null)
                    {
                        comboUI.DisableUI();
                    }
                }
            }
            else
            {
                _rdy.AddForce(Vector3.up.normalized * (_impulse), ForceMode.Impulse);
            }
            
            Reset();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if(_mainMenu==false)
        {
            _finalMenuAnimator.SetTrigger("upScaling");
            _finalMenuText.text = totalScore.ToString();
            _rdy.velocity = Vector3.zero;
            dead = true;
            
        }
    }

    private void Reset()
    {
        if(comboScore!=0)
        {
            UpdateScore(comboScore);
            comboScore = 0;
        }
            
        foreach (var maneuvers in _uiManuversList)
        {
            maneuvers.DisableUI();

        }
            
        _uiManuversList.Clear();
    }

    private void CreateUI(string manauever, int score)
    {
        var comboUI = Instantiate(_uiElementCombo, _uiElementScoreRoot);
        comboUI.UpdateUI(manauever);
        comboScore+=score;
        _uiManuversList.Add(comboUI);
    }
    
    public void Pause()
    {
        if(_rdy.IsSleeping())
        {
            _rdy.WakeUp();
            _uiPause.SetTrigger("downScaling");
        }
        else
        {
            _rdy.Sleep();

            _uiPause.SetTrigger("upScaling");
        }
            //Time.timeScale = 0;
    }
}
