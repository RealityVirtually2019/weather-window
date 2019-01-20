using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;



public class weatherData : MonoBehaviour
{
    private string cityName = "London";
    private string countryCode = "uk";
    private string weatherDescription;
    private float temp;
    private float tempMin;
    private float tempMax;
    private float rain;
    private float snow;
    private float humidity;
    private float sunrise;
    private float sunset;

    public Text cityInformation;
    public Text weatherInformation;
    public Text sunriseInformation;
    public Text sunsetInformation;
    public Text tempInformation;
    public Text tempMinInformation;
    public Text tempMaxInformation;

    private string weatherDescriptionForecast;
    private string timeForecast;
    private float tempForecast;
    private float tempMinForecast;
    private float tempMaxForecast;
    private float windspeedForecast;
    private float rainForecast;
    private float snowForecast;

    private float fahrenheit;

    public Slider mainSlider;
    public Button firstButton, secondButton, thirdButton;
   
    private string APPID;
    private string url;

    public GameObject raining;
    public GameObject snowing;
    //public GameObject sunny;

    public GameObject NewYork;
    public GameObject London;
    public GameObject Paris;


    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        firstButton.onClick.AddListener(delegate { TaskOnClick("first"); });
        secondButton.onClick.AddListener(delegate { TaskOnClick("second"); });
        thirdButton.onClick.AddListener(delegate { TaskOnClick("third"); });

        APPID = "&appid=08d5f1e9858dc88e49fe968e268f17a5";
        url = "http://api.openweathermap.org/data/2.5/";

        StartCoroutine(getWeatherData());
        StartCoroutine(getForecastData(0));
    }

    IEnumerator getWeatherData()
    {
        WWW weatherRequest = new WWW(url + "weather?q=" + cityName + "," + countryCode + APPID);

        Debug.Log(weatherRequest);

        yield return weatherRequest;
        if (weatherRequest.error == null || weatherRequest.error == "")
        {
            setWeatherAttributes(weatherRequest.text);
        }
        else
        {
            Debug.Log("ERROR: " + weatherRequest.error);
        }
    }

    IEnumerator getForecastData(int n)
    {
        WWW forecastRequest = new WWW(url + "forecast?q=" + cityName + "," + countryCode + APPID);
        yield return forecastRequest;
        if (forecastRequest.error == null || forecastRequest.error == "")
        {
            setForecastAttributes(forecastRequest.text, n);
        }
        else
        {
            Debug.Log("ERROR: " + forecastRequest.error);
        }
    }

    void setWeatherAttributes(string jsonString)
    {
        var weatherJson = JSON.Parse(jsonString);

        cityName = weatherJson["name"].Value;
        weatherDescription = weatherJson["weather"][0]["description"].Value;
        temp = weatherJson["main"]["temp"].AsFloat;
        tempMin = weatherJson["main"]["temp_min"].AsFloat;
        tempMax = weatherJson["main"]["temp_max"].AsFloat;
        //rain = weatherJson["main"]["rain"].AsFloat;
        //snow = weatherJson["main"]["snow"].AsFloat;
        //humidity = weatherJson["main"]["humidity"].AsFloat;
        sunrise = weatherJson["sys"]["sunrise"].AsFloat;
        sunset = weatherJson["sys"]["sunset"].AsFloat;

        Debug.Log("city: " + cityName);
        Debug.Log("weatherDescription: " + weatherDescription);
        Debug.Log("temp: " + temp);
        Debug.Log("tempMin: " + tempMin);
        Debug.Log("tempMax: " + tempMax);
        //Debug.Log("rain: " + rain);
        //Debug.Log("snow: " + snow);
        //Debug.Log("humidity: " + humidity);
        Debug.Log("sunrise: " + sunrise);
        Debug.Log("sunset: " + sunset);

        cityInformation.text = cityName;
        weatherInformation.text = weatherDescription;
        sunriseInformation.text = "sunrise: " + sunrise.ToString();
        sunsetInformation.text = "sunset: " + sunset.ToString();
        tempInformation.text = convertKtoF(temp).ToString() + "° F";
        tempMinInformation.text = convertKtoF(tempMin).ToString() + "° F";
        tempMaxInformation.text = convertKtoF(tempMax).ToString() + "° F";

        if (cityName == "New York")
        {
            Debug.Log("It's New York!");
            NewYork.SetActive(true);
        }
        else
        {
            NewYork.SetActive(false);
        }

        if (cityName == "London")
        {
            Debug.Log("It's London!");
            London.SetActive(true);
        }
        else
        {
            London.SetActive(false);
        }

        if (cityName == "Paris")
        {
            Debug.Log("It's Paris!");
            Paris.SetActive(true);
        }
        else
        {
            Paris.SetActive(false);
        }


        Debug.Log("end of weather results");

    }

    void setForecastAttributes(string jsonString, int n)
    {
        var weatherJson = JSON.Parse(jsonString);
       
            Debug.Log("what time segment: " + n);
            //forecast
            timeForecast = weatherJson["list"][n]["dt_txt"].Value;
            weatherDescriptionForecast = weatherJson["list"][n]["weather"][0]["description"].Value;
            tempForecast = weatherJson["list"][n]["main"]["temp"].AsFloat;
            tempMinForecast = weatherJson["list"][n]["main"]["temp_min"].AsFloat;
            tempMaxForecast = weatherJson["list"][n]["main"]["temp_max"].AsFloat;
            windspeedForecast = weatherJson["list"][n]["wind"]["speed"].AsFloat;
            rainForecast = weatherJson["list"][n]["rain"]["3h"];
            snowForecast = weatherJson["list"][n]["snow"]["3h"];

            Debug.Log("timeForecast: " + n + timeForecast);
            Debug.Log("weatherDescriptionForecast: " + n + weatherDescriptionForecast);
            Debug.Log("tempForecast: " + n + tempForecast);
            Debug.Log("tempMinForecast: " + n + tempMinForecast);
            Debug.Log("tempMaxForecast: " + n + tempMaxForecast);
            Debug.Log("windspeedForecast: " + n + windspeedForecast);
            Debug.Log("rainForecast: " + rainForecast);
            Debug.Log("snowForecast: " + snowForecast);

            weatherInformation.text = weatherDescriptionForecast;
            sunriseInformation.text = "sunrise: " + sunrise.ToString();
            sunsetInformation.text = "sunset: " + sunset.ToString();
            tempInformation.text = convertKtoF(tempForecast).ToString() + "° F";
            tempMinInformation.text = convertKtoF(tempMinForecast).ToString() + "° F";
            tempMaxInformation.text = convertKtoF(tempMaxForecast).ToString() + "° F";
     

        if (rainForecast> 0)
        {
            Debug.Log("It's raining!!!");
            raining.SetActive(true);
        }
        else
        {
            Debug.Log("not raining");
            raining.SetActive(false);
        }

        if (snowForecast > 0)
        {
            Debug.Log("It's snowing!!!");
            snowing.SetActive(true);
        }
        else
        {
            Debug.Log("not snowing");
            snowing.SetActive(false);
        }

        if (rainForecast == 0 && snowForecast == 0)
        {
            Debug.Log("It's sunny!");
            //sunny.SetActive(true);
        }
        else
        {
            Debug.Log("not sunny");
            //sunny.SetActive(false);
        }

        Debug.Log("end of forecast results");
         
    }

    float convertKtoF(float temperature)
    {

        fahrenheit = (float)(1.8 * (temperature - 273) + 32);

        var roundedNumber = Mathf.Round(fahrenheit);
        return roundedNumber;
    }

    public void ValueChangeCheck()
    {
        Debug.Log("slider value is: " + mainSlider.value);

        // 8different sets of data (24 hours)

        for (int i = 0; i < 8; i++)
        {
      
            if (mainSlider.value >= i / 8f && mainSlider.value <= (i+1) / 8f)
            {
                StartCoroutine(getForecastData(i));
                Debug.Log("getting the data: " + i);
            }

        }
    }

    void TaskOnClick(string buttonNum)
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");

        if (buttonNum == "first")
        {
            Debug.Log("second button - New York");
            cityName = "New York";
            countryCode = "US";
            StartCoroutine(getWeatherData());
            StartCoroutine(getForecastData(0));
        }
        if (buttonNum == "second")
        {
            Debug.Log("first button - London");
            cityName = "London";
            countryCode = "UK";
            StartCoroutine(getWeatherData());
            StartCoroutine(getForecastData(0));
        }
        if (buttonNum == "third")
        {
            Debug.Log("third button - Paris");
            cityName = "Paris";
            countryCode = "FR";
            StartCoroutine(getWeatherData());
            StartCoroutine(getForecastData(0));
        }
    }
}