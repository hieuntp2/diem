using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyClass
{
    public enum MaMon
    {
        TOAN = 1,
        VAT_LY = 2,
        HOA_HOC = 3,
        SINH_HOC = 4,
        NGU_VAN = 5,
        LICH_SU = 6,
        DIA_LY = 7,
        T_ANH = 8,
        T_NGA = 9,
        T_PHAP = 10,
        T_TRUNG = 11,
        T_DUC = 12,
        T_NHAT = 13,
        NANG_KHIEU_VE_1 = 14,
        NANG_KHIEU_VE_2 = 15,
        VE_MY_THUAT = 16,
        NANG_KHIEU_AM_NHAC_1 = 17,
        NANG_KHIEU_AM_NHAC_2 = 18,
        DOC_DIEN_CAM = 19,
        TDTT = 20,
        NK_SKDA_1 = 21,
        NK_SKDA_2 = 22,
        NANG_KHIEU_BAO_CHI = 23,
        KY_THUAT_NGHE = 24
    }

    public class MyConvertMonThi
    {
    }

    public class MyToHopMon
    {
        public Dictionary<string, List<string>> tohopmons;
        public MyToHopMon()
        {
            tohopmons.Add("A00", new List<string>() { "1", "2", "3" });
            tohopmons.Add("A01", new List<string>() { "1", "2", "8" });
            tohopmons.Add("B00", new List<string>() { "1", "3", "4" });
            tohopmons.Add("C00", new List<string>() { "5", "6", "7" });
            tohopmons.Add("D01", new List<string>() { "5", "1", "8" });
            tohopmons.Add("D02", new List<string>() { "5", "1", "9" });
            tohopmons.Add("D03", new List<string>() { "5", "1", "10" });
            tohopmons.Add("D04", new List<string>() { "5", "1", "11" });
            tohopmons.Add("D05", new List<string>() { "5", "1", "12" });
            tohopmons.Add("D06", new List<string>() { "5", "1", "13" });
            tohopmons.Add("A02", new List<string>() { "1", "2", "4" });
            tohopmons.Add("A03", new List<string>() { "1", "2", "6" });
            tohopmons.Add("A04", new List<string>() { "1", "2", "7" });
            tohopmons.Add("A05", new List<string>() { "1", "3", "6" });
            tohopmons.Add("A06", new List<string>() { "1", "3", "7" });
            tohopmons.Add("A07", new List<string>() { "1", "6", "7" });
            tohopmons.Add("B01", new List<string>() { "1", "4", "6" });
            tohopmons.Add("B02", new List<string>() { "1", "4", "7" });
            tohopmons.Add("B03", new List<string>() { "1", "4", "5" });
            tohopmons.Add("C01", new List<string>() { "5", "1", "2" });
            tohopmons.Add("C02", new List<string>() { "5", "1", "3" });
            tohopmons.Add("C03", new List<string>() { "5", "1", "6" });
            tohopmons.Add("C04", new List<string>() { "5", "1", "7" });
            tohopmons.Add("C05", new List<string>() { "5", "2", "3" });
            tohopmons.Add("C06", new List<string>() { "5", "2", "4" });
            tohopmons.Add("C07", new List<string>() { "5", "2", "6" });
            tohopmons.Add("C08", new List<string>() { "5", "3", "4" });
            tohopmons.Add("C09", new List<string>() { "5", "2", "7" });
            tohopmons.Add("C10", new List<string>() { "5", "3", "6" });
            tohopmons.Add("C11", new List<string>() { "5", "3", "7" });
            tohopmons.Add("C12", new List<string>() { "5", "4", "6" });
            tohopmons.Add("C13", new List<string>() { "5", "4", "7" });
            tohopmons.Add("D07", new List<string>() { "1", "3", "8" });
            tohopmons.Add("D08", new List<string>() { "1", "4", "8" });
            tohopmons.Add("D09", new List<string>() { "1", "6", "8" });
            tohopmons.Add("D10", new List<string>() { "1", "7", "8" });
            tohopmons.Add("D11", new List<string>() { "5", "2", "8" });
            tohopmons.Add("D12", new List<string>() { "5", "3", "8" });
            tohopmons.Add("D13", new List<string>() { "5", "4", "8" });
            tohopmons.Add("D14", new List<string>() { "5", "6", "8" });
            tohopmons.Add("D15", new List<string>() { "5", "7", "8" });
            tohopmons.Add("D16", new List<string>() { "1", "7", "12" });
            tohopmons.Add("D17", new List<string>() { "1", "7", "9" });
            tohopmons.Add("D18", new List<string>() { "1", "7", "13" });
            tohopmons.Add("D19", new List<string>() { "1", "7", "10" });
            tohopmons.Add("D20", new List<string>() { "1", "7", "11" });
            tohopmons.Add("D21", new List<string>() { "1", "3", "12" });
            tohopmons.Add("D22", new List<string>() { "1", "3", "9" });
            tohopmons.Add("D23", new List<string>() { "1", "3", "13" });
            tohopmons.Add("D24", new List<string>() { "1", "3", "10" });
            tohopmons.Add("D25", new List<string>() { "1", "3", "11" });
            tohopmons.Add("D26", new List<string>() { "1", "2", "12" });
            tohopmons.Add("D27", new List<string>() { "1", "2", "9" });
            tohopmons.Add("D28", new List<string>() { "1", "2", "13" });
            tohopmons.Add("D29", new List<string>() { "1", "2", "10" });
            tohopmons.Add("D30", new List<string>() { "1", "2", "11" });
            tohopmons.Add("D31", new List<string>() { "1", "4", "12" });
            tohopmons.Add("D32", new List<string>() { "1", "4", "9" });
            tohopmons.Add("D33", new List<string>() { "1", "4", "13" });
            tohopmons.Add("D34", new List<string>() { "1", "4", "10" });
            tohopmons.Add("D35", new List<string>() { "1", "4", "11" });
            tohopmons.Add("D36", new List<string>() { "1", "6", "12" });
            tohopmons.Add("D37", new List<string>() { "1", "6", "9" });
            tohopmons.Add("D38", new List<string>() { "1", "6", "13" });
            tohopmons.Add("D39", new List<string>() { "1", "6", "10" });
            tohopmons.Add("D40", new List<string>() { "1", "6", "11" });
            tohopmons.Add("D41", new List<string>() { "5", "7", "12" });
            tohopmons.Add("D42", new List<string>() { "5", "7", "9" });
            tohopmons.Add("D43", new List<string>() { "5", "7", "13" });
            tohopmons.Add("D44", new List<string>() { "5", "7", "10" });
            tohopmons.Add("D45", new List<string>() { "5", "7", "11" });
            tohopmons.Add("D46", new List<string>() { "5", "3", "12" });
            tohopmons.Add("D47", new List<string>() { "5", "3", "9" });
            tohopmons.Add("D48", new List<string>() { "5", "3", "13" });
            tohopmons.Add("D49", new List<string>() { "5", "3", "10" });
            tohopmons.Add("D50", new List<string>() { "5", "3", "11" });
            tohopmons.Add("D51", new List<string>() { "5", "2", "12" });
            tohopmons.Add("D52", new List<string>() { "5", "2", "9" });
            tohopmons.Add("D53", new List<string>() { "5", "2", "13" });
            tohopmons.Add("D54", new List<string>() { "5", "2", "10" });
            tohopmons.Add("D55", new List<string>() { "5", "2", "11" });
            tohopmons.Add("D56", new List<string>() { "5", "4", "12" });
            tohopmons.Add("D57", new List<string>() { "5", "4", "9" });
            tohopmons.Add("D58", new List<string>() { "5", "4", "13" });
            tohopmons.Add("D59", new List<string>() { "5", "4", "10" });
            tohopmons.Add("D60", new List<string>() { "5", "4", "11" });
            tohopmons.Add("D61", new List<string>() { "5", "6", "12" });
            tohopmons.Add("D62", new List<string>() { "5", "6", "9" });
            tohopmons.Add("D63", new List<string>() { "5", "6", "13" });
            tohopmons.Add("D64", new List<string>() { "5", "6", "10" });
            tohopmons.Add("D65", new List<string>() { "5", "6", "11" });
            tohopmons.Add("H00", new List<string>() { "5", "14", "15" });
            tohopmons.Add("H01", new List<string>() { "1", "5", "16" });
            tohopmons.Add("N00", new List<string>() { "5", "17", "18" });
            tohopmons.Add("M00", new List<string>() { "5", "1", "19" });
            tohopmons.Add("T00", new List<string>() { "1", "4", "20" });
            tohopmons.Add("V00", new List<string>() { "1", "2", "16" });
            tohopmons.Add("V01", new List<string>() { "1", "5", "16" });
            tohopmons.Add("S00", new List<string>() { "5", "21", "22" });
            tohopmons.Add("R00", new List<string>() { "5", "6", "23" });
            tohopmons.Add("K00", new List<string>() { "1", "2", "24" });
        }
    }
}