using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LabManagementSystem.Services;

namespace LabManagementSystem.Printing
{
    public enum UrineSection
    {
        Physical,
        Chemical,
        MicroscopicCells,
        MicroscopicOther,
        Microorganisms
    }

    public enum CbcSection
    {
        MainRbc,
        MainPlatelet,
        WbcTotal,
        WbcDifferential,
        Other
    }

    /// <summary>
    /// Medical laboratory English translator, term normalizer, and test/parameter classifier.
    /// Ensures 100% pure English output on laboratory reports with professional clinical terminology.
    /// </summary>
    public static class LabReportEnglishHelper
    {
        public static bool IsUrineAnalysis(ConsolidatedTestDto test)
        {
            if (test == null) return false;
            string name = (test.TestName ?? "").Trim().ToLowerInvariant();
            string cat = (test.CategoryName ?? "").Trim().ToLowerInvariant();

            if (name.Contains("urine") || name.Contains("urinalysis") || name.Contains("بول"))
                return true;
            if (cat.Contains("urine") || cat.Contains("بول"))
                return true;

            // Check if parameters look like urine analysis
            int matches = 0;
            foreach (var p in test.Parameters)
            {
                string pn = (p.ParameterName ?? "").ToLowerInvariant();
                if (pn.Contains("aspect") || pn.Contains("odor") || pn.Contains("sediment") ||
                    pn.Contains("pus") || pn.Contains("epithelial") || pn.Contains("crystals") ||
                    pn.Contains("urobilinogen") || pn.Contains("specific gravity") || pn.Contains("amorphous") ||
                    pn.Contains("مظهر") || pn.Contains("راسب") || pn.Contains("صديد"))
                {
                    matches++;
                }
            }
            return matches >= 2;
        }

        public static bool IsCbc(ConsolidatedTestDto test)
        {
            if (test == null) return false;
            string name = (test.TestName ?? "").Trim().ToLowerInvariant();
            string cat = (test.CategoryName ?? "").Trim().ToLowerInvariant();

            if (name.Contains("cbc") || name.Contains("blood count") || name.Contains("صورة دم") || name.Contains("hemogram"))
                return true;

            int matches = 0;
            foreach (var p in test.Parameters)
            {
                string pn = (p.ParameterName ?? "").ToLowerInvariant();
                if (pn.Contains("hemoglobin") || pn.Contains("hct") || pn.Contains("mcv") ||
                    pn.Contains("mch") || pn.Contains("platelet") || pn.Contains("rdw") ||
                    pn.Contains("wbc") || pn.Contains("lym") || pn.Contains("gra") ||
                    pn.Contains("هيموجلوبين") || pn.Contains("صفائح"))
                {
                    matches++;
                }
            }
            return matches >= 3;
        }

        public static string NormalizeTestName(string testName)
        {
            if (string.IsNullOrWhiteSpace(testName)) return "LABORATORY REPORT";
            string trimmed = testName.Trim();

            if (trimmed.Contains("صورة دم", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Contains("cbc", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Contains("blood count", StringComparison.OrdinalIgnoreCase))
            {
                return "COMPLETE BLOOD COUNT";
            }

            if (trimmed.Contains("تحليل بول", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Contains("urine", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Contains("urinalysis", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Contains("بول", StringComparison.OrdinalIgnoreCase))
            {
                return "Urine Analysis";
            }

            if (trimmed.Contains("وظائف كبد", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("كبد", StringComparison.OrdinalIgnoreCase))
                return "LIVER FUNCTION TESTS";
            if (trimmed.Contains("وظائف كلى", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("كلى", StringComparison.OrdinalIgnoreCase))
                return "KIDNEY FUNCTION TESTS";
            if (trimmed.Contains("سكر صائم", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("fasting", StringComparison.OrdinalIgnoreCase))
                return "FASTING BLOOD GLUCOSE";
            if (trimmed.Contains("سكر تراكمي", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("hba1c", StringComparison.OrdinalIgnoreCase))
                return "HbA1c GLYCATED HEMOGLOBIN";
            if (trimmed.Contains("دهون", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("lipid", StringComparison.OrdinalIgnoreCase))
                return "LIPID PROFILE";
            if (trimmed.Contains("غدة", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("thyroid", StringComparison.OrdinalIgnoreCase))
                return "THYROID PROFILE";

            return TransliterateOrTranslateToEnglish(trimmed).ToUpperInvariant();
        }

        public static string NormalizeParameterName(string paramName)
        {
            if (string.IsNullOrWhiteSpace(paramName)) return "";
            string clean = paramName.Trim();

            // Urine Parameters
            if (IsMatch(clean, "volume", "حجم", "كمية")) return "Volume";
            if (IsMatch(clean, "color", "urine color", "لون", "لون البول")) return "Color";
            if (IsMatch(clean, "odor", "urine odor", "رائحة", "رائحة البول")) return "Odor";
            if (IsMatch(clean, "aspect", "appearance", "مظهر", "مظهر البول", "شفافية")) return "Aspect";
            if (IsMatch(clean, "foam", "الرغوة", "رغوة")) return "Foam";
            if (IsMatch(clean, "sediment", "راسب", "الراسب")) return "Sediment";

            if (IsMatch(clean, "reaction", "reaction (ph)", "ph", "حموضة", "تفاعل")) return "Reaction (pH)";
            if (IsMatch(clean, "specific gravity", "s.g", "sp. gravity", "الكثافة النوعية", "كثافة")) return "Specific gravity";
            if (IsMatch(clean, "leukocyte esterase", "esterase", "إستريز", "استريز")) return "Leukocyte esterase";
            if (IsMatch(clean, "blood", "دم", "دم خفي")) return "Blood";
            if (IsMatch(clean, "glucose", "sugar", "سكر", "جلوكوز")) return "Glucose";
            if (IsMatch(clean, "protein", "albumin", "زلال", "بروتين")) return "Protein";
            if (IsMatch(clean, "ketones", "acetone", "أسيتون", "كيتون")) return "Ketones";
            if (IsMatch(clean, "nitrite", "نيتريت")) return "Nitrite";
            if (IsMatch(clean, "bilirubin", "بيليروبين", "صفراء")) return "Bilirubin";
            if (IsMatch(clean, "urobilinogen", "يوروبيلينوجين")) return "Urobilinogen";

            if (IsMatch(clean, "rbcs", "rbc", "خلايا دم حمراء", "كرات دم حمراء")) return "RBCs";
            if (IsMatch(clean, "pus", "pus cells", "خلايا صديدية", "صديد")) return "Pus";
            if (IsMatch(clean, "epithelial", "epithelial cells", "خلايا طلائية", "خلايا بشرية")) return "Epithelial cells";
            if (IsMatch(clean, "casts", "اسطوانات", "إسطوانات")) return "Casts";
            if (IsMatch(clean, "amorphous", "أمورفوس", "أملاح غير متشكلة")) return "Amorphous";
            if (IsMatch(clean, "crystals", "بلورات", "كريستال")) return "Crystals";
            if (IsMatch(clean, "bacteria", "بكتيريا")) return "Bacteria";
            if (IsMatch(clean, "fungus", "yeast", "فطريات", "خميرة")) return "Fungus";
            if (IsMatch(clean, "mucus", "مخاط")) return "Mucus";
            if (IsMatch(clean, "others", "أخرى", "اخرى")) return "Others";

            // CBC Parameters
            if (IsMatch(clean, "hemoglobin", "hb", "heamoglobin", "هيموجلوبين")) return "Hemoglobin";
            if (IsMatch(clean, "rbc-erythrocytes", "erythrocytes", "rbc count", "كرات دم حمراء")) return "RBC-Erythrocytes";
            if (IsMatch(clean, "hct-hematocrit", "hematocrit", "hct", "pcv", "هيماتوكريت")) return "Hct-Hematocrit";
            if (IsMatch(clean, "mcv")) return "MCV";
            if (IsMatch(clean, "mch")) return "MCH";
            if (IsMatch(clean, "mchc")) return "MCHC";
            if (IsMatch(clean, "rdws", "rdw-s", "rdw-sd", "rdw s")) return "RDWs";
            if (IsMatch(clean, "rdwc", "rdw-c", "rdw-cv", "rdw c")) return "RDWc";

            if (IsMatch(clean, "platelet", "platelets", "plt", "صفائح", "صفائح دموية")) return "Platelet";
            if (IsMatch(clean, "pct-plateletcrit", "plateletcrit", "pct")) return "PCT-Plateletcrit";
            if (IsMatch(clean, "pdws", "pdw-s")) return "PDWs";
            if (IsMatch(clean, "pdwc", "pdw-c", "pdw")) return "PDWc";
            if (IsMatch(clean, "mpv")) return "MPV";
            if (IsMatch(clean, "p-lcc", "plcc")) return "P-LCC";
            if (IsMatch(clean, "p-lcr", "plcr")) return "P-LCR";

            if (IsMatch(clean, "wbc-leukocytes", "wbc", "wbcs", "leukocytes", "white blood cells", "كرات دم بيضاء")) return "WBC-Leukocytes";
            if (IsMatch(clean, "lym %", "lym", "lymphocytes", "lymphocytes %", "ليمفوسايت")) return "LYM %";
            if (IsMatch(clean, "mid %", "mid", "monocytes", "خلايا وسطى")) return "MID %";
            if (IsMatch(clean, "gra %", "gra", "granulocytes", "neutrophils", "خلايا محببة")) return "GRA %";
            if (IsMatch(clean, "lym #", "lymphocytes abs", "lym abs", "lym count")) return "LYM #";
            if (IsMatch(clean, "mid #", "monocytes abs", "mid abs")) return "MID #";
            if (IsMatch(clean, "gra #", "granulocytes abs", "gra abs")) return "GRA #";

            return TransliterateOrTranslateToEnglish(clean);
        }

        public static UrineSection ClassifyUrineParameter(string paramName, string? categoryName = null)
        {
            string p = (paramName ?? "").ToLowerInvariant().Trim();
            string c = (categoryName ?? "").ToLowerInvariant().Trim();

            if (c.Contains("physical") || c.Contains("فيزيائي") || c.Contains("ظاهري")) return UrineSection.Physical;
            if (c.Contains("chemical") || c.Contains("كيميائي")) return UrineSection.Chemical;
            if (c.Contains("microscopic") || c.Contains("مجهري"))
            {
                if (p.Contains("bacteria") || p.Contains("fungus") || p.Contains("mucus") || p.Contains("yeast") || p.Contains("بكتيريا") || p.Contains("فطريات"))
                    return UrineSection.Microorganisms;
                if (p.Contains("rbc") || p.Contains("pus") || p.Contains("epithelial") || p.Contains("cells") || p.Contains("صديد") || p.Contains("حمراء"))
                    return UrineSection.MicroscopicCells;
                return UrineSection.MicroscopicOther;
            }

            if (p.Contains("volume") || p.Contains("color") || p.Contains("odor") || p.Contains("aspect") ||
                p.Contains("appearance") || p.Contains("foam") || p.Contains("sediment") ||
                p.Contains("لون") || p.Contains("مظهر") || p.Contains("رائحة") || p.Contains("راسب") || p.Contains("حجم"))
            {
                return UrineSection.Physical;
            }

            // Check Microscopic items before Chemical so words like "amorphous" or "phosphates" never match "ph"
            if (p.Contains("bacteria") || p.Contains("fungus") || p.Contains("yeast") || p.Contains("mucus") ||
                p.Contains("بكتيريا") || p.Contains("فطريات") || p.Contains("مخاط"))
            {
                return UrineSection.Microorganisms;
            }

            if (p.Contains("rbc") || p.Contains("pus") || p.Contains("epithelial") || p.Contains("cells") || p.Contains("صديد") || p.Contains("حمراء"))
            {
                return UrineSection.MicroscopicCells;
            }

            if (p.Contains("amorphous") || p.Contains("crystal") || p.Contains("cast") || p.Contains("بلورات") || p.Contains("اسطوانات") || p.Contains("امورف"))
            {
                return UrineSection.MicroscopicOther;
            }

            if (p.Contains("reaction") || p.Contains("gravity") || p.Contains("esterase") ||
                p.Contains("protein") || p.Contains("albumin") || p.Contains("glucose") || p.Contains("sugar") ||
                p.Contains("ketone") || p.Contains("acetone") || p.Contains("nitrite") || p.Contains("bilirubin") ||
                p.Contains("urobilinogen") || p.Contains("blood") || p.Contains("حموضة") || p.Contains("زلال") || p.Contains("سكر") ||
                p == "ph" || p.StartsWith("ph ") || p.EndsWith(" ph") || p.Contains("(ph)"))
            {
                return UrineSection.Chemical;
            }

            return UrineSection.MicroscopicOther;
        }

        public static CbcSection ClassifyCbcParameter(string paramName)
        {
            string p = (paramName ?? "").ToLowerInvariant().Trim();

            if (p.Contains("wbc-leukocytes") || p == "wbc" || p == "wbcs" || p.Contains("leukocytes") || p.Contains("كرات دم بيضاء"))
                return CbcSection.WbcTotal;

            if (p.Contains("lym") || p.Contains("mid") || p.Contains("gra") || p.Contains("lymphocyte") ||
                p.Contains("monocyte") || p.Contains("granulocyte") || p.Contains("neutrophil") ||
                p.Contains("differential"))
            {
                return CbcSection.WbcDifferential;
            }

            if (p.Contains("platelet") || p.Contains("pct") || p.Contains("pdw") || p.Contains("mpv") ||
                p.Contains("p-lcc") || p.Contains("p-lcr") || p.Contains("صفائح"))
            {
                return CbcSection.MainPlatelet;
            }

            return CbcSection.MainRbc;
        }

        public static string NormalizeResultValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return "-";
            string clean = value.Trim();

            // Arabic qualitative values translation
            if (IsMatch(clean, "أصفر", "اصفر", "yellow")) return "Yellow";
            if (IsMatch(clean, "أصفر قشي", "قشي", "straw yellow", "straw")) return "Straw Yellow";
            if (IsMatch(clean, "أصفر فاتح", "light yellow")) return "Light Yellow";
            if (IsMatch(clean, "أصفر داكن", "dark yellow")) return "Dark Yellow";

            if (IsMatch(clean, "عكر", "turbid")) return "Turbid";
            if (IsMatch(clean, "نصف عكر", "semi turbid", "slightly turbid")) return "Semi Turbid";
            if (IsMatch(clean, "رائق", "clear")) return "Clear";
            if (IsMatch(clean, "عطري", "aromatic", "arowmatic")) return "Aromatic";

            if (IsMatch(clean, "حمضي", "acidic")) return "Acidic";
            if (IsMatch(clean, "قلوي", "alkaline")) return "Alkaline";
            if (IsMatch(clean, "متعادل", "neutral")) return "Neutral";

            if (clean.StartsWith("قلوي", StringComparison.OrdinalIgnoreCase) || clean.StartsWith("alkaline", StringComparison.OrdinalIgnoreCase))
            {
                return Regex.Replace(clean, "قلوي", "Alkaline", RegexOptions.IgnoreCase);
            }

            if (IsMatch(clean, "سلبي", "سالب", "negative")) return "Negative";
            if (IsMatch(clean, "إيجابي", "ايجابي", "موجب", "positive")) return "Positive";
            if (IsMatch(clean, "طبيعي", "normal")) return "Normal";
            if (IsMatch(clean, "أثر", "اثر", "trace")) return "Trace";
            if (IsMatch(clean, "أثر طبيعي", "normal trace", "أثر خفيف")) return "Normal trace";
            if (IsMatch(clean, "لا يوجد", "لايوجد", "عدم وجود", "nil", "none")) return "Nil";
            if (IsMatch(clean, "غائب", "absent")) return "Absent";
            if (IsMatch(clean, "موجود", "present")) return "Present";
            if (IsMatch(clean, "قليل", "few")) return "Few";
            if (IsMatch(clean, "متوسط", "moderate")) return "Moderate";
            if (IsMatch(clean, "كثير", "many")) return "Many";

            if (clean.Contains("فوسفات غير متشكلة", StringComparison.OrdinalIgnoreCase) || clean.Contains("am. phosphates", StringComparison.OrdinalIgnoreCase))
            {
                return clean.Replace("فوسفات غير متشكلة", "Am. phosphates").Replace("أملاح فوسفات", "Am. phosphates");
            }

            if (clean.Contains("يورات غير متشكلة", StringComparison.OrdinalIgnoreCase) || clean.Contains("am. urates", StringComparison.OrdinalIgnoreCase))
            {
                return clean.Replace("يورات غير متشكلة", "Am. urates").Replace("أملاح يورات", "Am. urates");
            }

            if (clean.Contains("منتصف البول", StringComparison.OrdinalIgnoreCase) || clean.Contains("عينة عشوائية", StringComparison.OrdinalIgnoreCase))
            {
                return "Random Sample (Midstream urine)";
            }

            return clean;
        }

        public static string NormalizeNormalRange(string range, string? normalText = null)
        {
            string clean = (range ?? normalText ?? "").Trim();
            if (string.IsNullOrWhiteSpace(clean)) return "-";

            if (IsMatch(clean, "yellow", "أصفر")) return "Yellow";
            if (IsMatch(clean, "aromatic", "arowmatic", "عطري")) return "Aromatic";
            if (IsMatch(clean, "clear", "رائق")) return "Clear";
            if (IsMatch(clean, "nil / absent", "nil/absent", "لا يوجد / غائب")) return "Nil / Absent";
            if (IsMatch(clean, "nil", "لا يوجد")) return "Nil";
            if (IsMatch(clean, "absent", "غائب")) return "Absent";
            if (IsMatch(clean, "negative", "سلبي")) return "Negative";
            if (IsMatch(clean, "normal", "طبيعي")) return "Normal";
            if (IsMatch(clean, "normal trace", "أثر خفيف", "أثر طبيعي")) return "Normal trace";
            if (IsMatch(clean, "nil / few", "nil/few", "قليل / لا يوجد")) return "Nil / Few";

            // Clean up Arabic digits if any
            clean = clean.Replace('٠', '0').Replace('١', '1').Replace('٢', '2').Replace('٣', '3').Replace('٤', '4')
                         .Replace('٥', '5').Replace('٦', '6').Replace('٧', '7').Replace('٨', '8').Replace('٩', '9');

            return clean;
        }

        public static string NormalizeUnit(string? unit)
        {
            if (string.IsNullOrWhiteSpace(unit)) return "";
            string clean = unit.Trim();

            if (clean.Equals("g/dl", StringComparison.OrdinalIgnoreCase) || clean.Equals("g/dL", StringComparison.OrdinalIgnoreCase)) return "g/dl";
            if (clean.Equals("fl", StringComparison.OrdinalIgnoreCase) || clean.Equals("fi", StringComparison.OrdinalIgnoreCase)) return "fl";
            if (clean.Equals("pg", StringComparison.OrdinalIgnoreCase)) return "pg";
            if (clean.Equals("%", StringComparison.OrdinalIgnoreCase)) return "%";
            if (clean.Contains("10^12", StringComparison.OrdinalIgnoreCase)) return "10^12/L";
            if (clean.Contains("10^9", StringComparison.OrdinalIgnoreCase)) return "10^9/L";
            if (clean.Contains("cells", StringComparison.OrdinalIgnoreCase) || clean.Contains("hpf", StringComparison.OrdinalIgnoreCase) || clean.Contains("h.p.f", StringComparison.OrdinalIgnoreCase))
                return "Cells/H.P.F";

            return clean;
        }

        private static bool IsMatch(string input, params string[] options)
        {
            foreach (var opt in options)
            {
                if (string.Equals(input, opt, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private static string TransliterateOrTranslateToEnglish(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            // Check if contains Arabic characters
            bool hasArabic = Regex.IsMatch(text, @"[\u0600-\u06FF]");
            if (!hasArabic) return text;

            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["صورة دم كاملة"] = "Complete Blood Count",
                ["صورة دم"] = "Blood Picture",
                ["تحليل بول كامل"] = "Urine Analysis",
                ["تحليل بول"] = "Urine Analysis",
                ["بول"] = "Urine",
                ["هيموجلوبين"] = "Hemoglobin",
                ["كرات الدم الحمراء"] = "RBC - Erythrocytes",
                ["كرات دم حمراء"] = "RBC - Erythrocytes",
                ["كرات الدم البيضاء"] = "WBC - Leukocytes",
                ["كرات دم بيضاء"] = "WBC - Leukocytes",
                ["الصفائح الدموية"] = "Platelets",
                ["صفائح دموية"] = "Platelets",
                ["فحص فيزيائي"] = "Physical Examination",
                ["فحص كيميائي"] = "Chemical Examination",
                ["فحص مجهري"] = "Microscopic Examination",
                ["وظائف كبد"] = "Liver Function Tests",
                ["وظائف كلى"] = "Kidney Function Tests",
                ["تحليل سكر"] = "Blood Glucose Test",
                ["سكر صائم"] = "Fasting Blood Glucose",
                ["سكر فاطر"] = "Post Prandial Blood Glucose",
                ["سكر عشوائي"] = "Random Blood Glucose",
                ["سكر تراكمي"] = "HbA1c Glycated Hemoglobin",
                ["دهون ثلاثية"] = "Triglycerides",
                ["كوليسترول كلي"] = "Total Cholesterol",
                ["حمض البوليك"] = "Uric Acid",
                ["يوريا الدم"] = "Blood Urea",
                ["كرياتينين"] = "Serum Creatinine",
                ["فيتامين د"] = "Vitamin D"
            };

            foreach (var kvp in map)
            {
                if (text.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                    return kvp.Value;
            }

            return "Laboratory Test";
        }
    }
}
