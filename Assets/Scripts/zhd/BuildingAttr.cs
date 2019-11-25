using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttr
{
    public enum RegionType { Campus, UC, NA, SHAW, WYS, WS, CWC, MC, SHHO, CC };
    public enum BuildingType {uniBuilding,dorBuilding,others };
    public RegionType region;
    public BuildingType buildingType;

    public string name;
    public float height = 0;

    public void setHeight(float h)
    {
        height = h;
    }

    public void setRegion(string regionName)
    {
        switch (regionName)
        {
            case "Chung Chi College":
                region = RegionType.CC;
                break;
            case "Wu Yee Sun College":
                region = RegionType.WYS;
                break;
            case "C.W. Chu College":
                region = RegionType.CWC;
                break;
            case "Morningside College":
                region = RegionType.MC;
                break;
            case "S.H. Ho College":
                region = RegionType.SHHO;
                break;
            case "United College":
                region = RegionType.UC;
                break;
            case "Shaw College":
                region = RegionType.SHAW;
                break;
            case "New Asia College":
                region = RegionType.NA;
                break;
            case "Lee Woo Sing College":
                region = RegionType.WS;
                break;
            case "Main Campus":
            case "Campus":
                region = RegionType.Campus;
                break;
        }
    }

}
