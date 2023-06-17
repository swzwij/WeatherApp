using System.Collections.Generic;

[System.Serializable]
public class Address
{
    public string house_number;
    public string road;
    public string city_district;
    public string town;
    public string state;
    public string country;
    public string postcode;
    public string country_code;
}

[System.Serializable]
public class LocationData
{
    public int place_id;
    public string licence;
    public string powered_by;
    public string osm_type;
    public int osm_id;
    public string lat;
    public string lon;
    public string display_name;
    public Address address;
    public List<string> boundingbox;
}