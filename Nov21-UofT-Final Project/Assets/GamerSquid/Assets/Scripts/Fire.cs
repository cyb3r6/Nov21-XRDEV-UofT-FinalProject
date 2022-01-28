/***
 * 
 *  A script for controlling the Prefab 'Torch-Fire'
 * 
***/

using UnityEngine;
using System.Collections;


public class Fire : MonoBehaviour {

    //Fire Game Objects (visible in the editor)
    [SerializeField]
    private GameObject _flame01;        //This game object has a particle system attached set to appear as a flame
    [SerializeField]
    private GameObject _flame02;        //This game object has a particle system attached set to appear as a flame
    [SerializeField]
    private GameObject _glow;           //This game object has a particle system attached set to appear as a rising glow
    [SerializeField]
    private GameObject _smokeDark;      //This game object has a particle system attached set to appear as dark colored smoke
    [SerializeField]
    private GameObject _smokeLight;     //This game object has a particle system attached set to appear as light colored smoke
    [SerializeField]
    private GameObject _sparksRising;   //This game object has a particle system attached set to appear as rising sparks
    [SerializeField]
    private GameObject _sparksFalling;  //This game object has a particle system attached set to appear as falling sparks
    [SerializeField]
    private Light _flickerLight;        //This game object has a light component (used for the flicker that the fire would give off)


    //Fire fining tuning settings (visible in the inspector)
    [SerializeField]
    private Color _flameColor = Color.white;                 //The intensity of the flame
    private Color _oldFlameColor;
    [SerializeField]
    [Range(0, 100)]
    public float _risingSparkAmount = 10f;              //Amount of rising sparks coming from the fire
    private float _oldRisingSparkAmount;
    [SerializeField]
    [Range(0, 100)]
    private float _fallingSparkAmount = 20f;            //Amount of falling sparks coming from the fire - burst up then fall
    private float _oldFallingSparkAmount;
    [SerializeField]
    [Range(0, 100)]
    private float _darkSmokeAmount = 16f;               //Amount of dark colored smoke
    private float _oldDarkSmokeAmount;
    [SerializeField]
    [Range(0, 100)]
    private float _lightSmokeAmount = 6f;               //Amount of light colored smoke
    private float _oldLightSmokeAmount;
    [SerializeField]
    private Color _flickerLightCol;                     //Color of the flickering light which will illuminate the scene
    private Color _oldFlickerLightCol;
    [SerializeField]
    [Range(0, 100)]
    private float _flickerIntensity;                    //Intensity/Brightness of the flickering light that will illuminate the scene
    private float _oldFlickerIntensity;

    private bool _isFlickering = true;                         //Is the flickering light on
    

    //Variables to feed the light flicker method
    private const float BASE_INTESITY = 0.7f;
    private const float MAX_REDUCTION = 0.2f;
    private const float MAX_INCREASE = 0.2f;
    private const float RATE_DAMPING = 0.1f;
    private const float STRENGTH = 300;
    private const float MAX_PARTICLE_SIZE = 0.248f;

    private ParticleSystemRenderer psr;

    // Initialize
    void Start () {

        //Initialise the fire (particle systems, lights etc)
        SetMaxParticleSize(MAX_PARTICLE_SIZE);
        SetRisingSparkAmount(_risingSparkAmount);
        SetFallingSparkAmount(_fallingSparkAmount);
        SetDarkSmokeAmount(_darkSmokeAmount);
        SetLightSmokeAmount(_lightSmokeAmount);
        SetFlickerLightColor(_flickerLightCol);
        SetFlickerIntensity(_flickerIntensity);

        _flickerLight.intensity = BASE_INTESITY;        //Override the lights intensity with a pre-defined value - a value to simulate light from a fire 

        turnOn();       //Start the fire
    }

    
    void Update ()
    {

        //Check for inspector updates
        if (_flameColor != _oldFlameColor)
        {
            SetFlameColor(_flameColor);
        }
        if (_fallingSparkAmount != _oldFallingSparkAmount)
        {
            SetFallingSparkAmount(_fallingSparkAmount);
        }
        if (_risingSparkAmount != _oldRisingSparkAmount)
        {
            SetRisingSparkAmount(_risingSparkAmount);
        }
        if (_darkSmokeAmount != _oldDarkSmokeAmount)
        {
            SetDarkSmokeAmount(_darkSmokeAmount);
        }
        if (_lightSmokeAmount != _oldLightSmokeAmount)
        {
            SetLightSmokeAmount(_lightSmokeAmount);
        }
        if (_flickerLightCol != _oldFlickerLightCol)
        {
            SetFlickerLightColor(_flickerLightCol);
        }
        if (_flickerIntensity != _oldFlickerIntensity)
        {
            SetFlickerIntensity(_flickerIntensity);
        }


    }

    /***
     *  Start the fire 
     ***/
    public void turnOn ()
    {
        //Turn on all children game object (Set active).
        _isFlickering = true;


        //Start the flickering effect
        StartCoroutine(DoFlicker());
    }

    /***
     *  Stop the fire 
     ***/
    public void turnOff()
    {
        //Turn off all children game object (Set inactive). Note, setting the parent GO to inactive (the GO that this script is attached to) will destroy this class and therefore cannot be turned back on
        _isFlickering = false;
    }

    /***
     *  Set the 'Flame Max Particle Amount'
     ***/
    public void SetMaxParticleSize(float maxSize)
    {
        psr = _flame02.GetComponent<ParticleSystemRenderer>();
        psr.maxParticleSize = maxSize;
        psr.minParticleSize = psr.maxParticleSize / 10f;

    }

    // Spark amount
    public float GetRisingSparkAmount()
    {
        return _risingSparkAmount;
    }

    
    /***
     *  Set the 'Flame Color' by manipulating the particle system
     ***/
    public void SetFlameColor(Color flameColor)
    {
        this._flameColor = flameColor;
        this._oldFlameColor = flameColor;

        ParticleSystem ps1 = _flame01.GetComponent<ParticleSystem>();
        ParticleSystem ps2 = _flame02.GetComponent<ParticleSystem>();
        var main1 = ps1.main;
        var main2 = ps2.main;
       // main1.startColor = flameColor;
        main2.startColor = flameColor;

    }

    // Spark amount
    public Color GetFlameColor()
    {
        return _flameColor;
    }

    /***
     *  Set the 'Rising Spark Amount' by manipulating the amount of particles (Max Particles) generated
     ***/
    public void SetRisingSparkAmount(float risingSparkAmount)
    {
        this._risingSparkAmount = risingSparkAmount;
        this._oldRisingSparkAmount = risingSparkAmount;

        ParticleSystem ps = _sparksRising.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.maxParticles = (int)risingSparkAmount;

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);     // Cannot set duration whilst particle system is playing
        main.duration = CalculateDuration(risingSparkAmount);
        ps.Play();      //Restart the particle system

    }

    public float GetFallingSparkAmount()
    {
        return _fallingSparkAmount;
    }

    /***
     *  Set the 'Falling Spark Amount' by manipulating the duration of the particle system. 
     *  The larger the 'Amount', the shorter the duration between bursts of falling sparks
     ***/
    public void SetFallingSparkAmount(float fallingSparkAmount)
    {
        this._fallingSparkAmount = fallingSparkAmount;          
        this._oldFallingSparkAmount = fallingSparkAmount;

        ParticleSystem ps = _sparksFalling.GetComponent<ParticleSystem>();  //Find the particle system attached to the falling sparks game object
        var main = ps.main;
        main.maxParticles = (int)fallingSparkAmount;

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);     // Cannot set duration whilst particle system is playing
        //main.duration = fallingSparkAmount < 100 ? (100f - fallingSparkAmount)/10 : 0.06f;
        main.duration = CalculateDuration(fallingSparkAmount);

        ps.Play();      //Restart the particle system
    }

    // Smoke amount
    public float GetDarkSmokeAmount()
    {
        return _darkSmokeAmount;
    }

    /***
     *  Set the 'Dark Smoke Amount' by manipulating the Rate Over Time of the particle system
     ***/
    public void SetDarkSmokeAmount(float darkSmokeAmount)
    {
        this._darkSmokeAmount = darkSmokeAmount;
        this._oldDarkSmokeAmount = darkSmokeAmount;

        ParticleSystem smokeDrkPS = _smokeDark.GetComponent<ParticleSystem>();      //Find the particle system attached to the dark smoke game object
        
        var emissionDrk = smokeDrkPS.emission;
        emissionDrk.rateOverTime = darkSmokeAmount * (darkSmokeAmount / 100);       //Update the rate over time to increase/decrease the amount of smoke
    }

    // Smoke amount
    public float GetLightSmokeAmount()
    {
        return _lightSmokeAmount;
    }

    /***
     *  Set the 'Light Smoke Amount' by manipulating the Rate Over Time of the particle system
     ***/
    public void SetLightSmokeAmount(float lightSmokeAmount)
    {
        this._lightSmokeAmount = lightSmokeAmount;
        this._oldLightSmokeAmount = lightSmokeAmount;

        ParticleSystem smokeLghtPS = _smokeLight.GetComponent<ParticleSystem>();    //Find the particle system attached to the light smoke game object

        var emissionLght = smokeLghtPS.emission;
        emissionLght.rateOverTime = lightSmokeAmount * (lightSmokeAmount / 100);    //Update the rate over time to increase/decrease the amount of smoke

    }

    

    // Light Flicker amount
    public Color GetFlickerLightColor()
    {
        return _flickerLightCol;
    }

    /***
     *  Set the light source color
     ***/
    public void SetFlickerLightColor(Color flickerLightCol)
    {
        this._flickerLightCol = flickerLightCol;
        this._oldFlickerLightCol = flickerLightCol;

        _flickerLight.color = flickerLightCol;
    }
    
    // Light Flicker intensity
    public float GetFlickerIntensity()
    {
        return _flickerIntensity;
    }

    /***
     *  Set the light source intensity. This acts as a multiplier of the calculated intesity of the flickering light.
     ***/
    public void SetFlickerIntensity(float flickerIntensity)
    {
        this._flickerIntensity = flickerIntensity;
        this._oldFlickerIntensity = flickerIntensity;

        _flickerLight.intensity = flickerIntensity;
    }

    private float CalculateDuration(float inputVal)
    {
        //The duration is set inverse to input controller value.  That is, as input goes higher, set duration lower
        float duration = 0f;

        //If the input is between 0 and 20
        if (inputVal > 0 && inputVal <= 10)
        {
            //Scale the input 10:1
            inputVal = inputVal / 10f;
        }
        else if (inputVal > 10 && inputVal <= 20)
        {
            //Scale the input 4:1
            inputVal = inputVal / 4f;
        }

        //inputVal = 100 - inputVal

        //When the input is 100 (max), set the duration to a very short period. 
        //THIS MUST BE GRATER THAN 0 otherwise the particle system will not play
        duration = inputVal < 100 ? (100f - inputVal) / 10 : 0.06f;
        
        return duration;
    }

    /***
     *  Flicker the light to simulate the fire
    ***/
    private IEnumerator DoFlicker()
    {
        float intensityMultiplier;
        _isFlickering = true;

        //While the light is flickering
        while (_isFlickering)
        {
            //Calculate the intensity multiplier
            intensityMultiplier = _flickerIntensity > 0 ? _flickerIntensity / 50 : 0;

            //Set the light source intensity to give the 'flicker' effect
            _flickerLight.intensity = Mathf.Lerp(_flickerLight.intensity, Random.Range(BASE_INTESITY - MAX_REDUCTION, BASE_INTESITY + MAX_INCREASE), STRENGTH * Time.deltaTime) * intensityMultiplier;
            yield return new WaitForSeconds(RATE_DAMPING);
        }
        _isFlickering = false;
    }

    

}
