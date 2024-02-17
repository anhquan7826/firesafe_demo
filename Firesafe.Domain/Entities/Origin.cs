using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Origin(string originId, string name)
{
    [MaxLength(2)] public string OriginId { get; init; } = originId;

    [MaxLength(50)] public string Name { get; init; } = name;

    // Navigations
    public IEnumerable<Product> Products { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Origin> builder)
    {
        builder.HasKey(o => o.OriginId);
    }

    public static void ConfigureInitialData(EntityTypeBuilder<Origin> builder)
    {
        builder.HasData(
            new Origin("af", "Afghanistan"),
            new Origin("ax", "Åland Islands"),
            new Origin("al", "Albania"),
            new Origin("dz", "Algeria"),
            new Origin("as", "American Samoa"),
            new Origin("ad", "Andorra"),
            new Origin("ao", "Angola"),
            new Origin("ai", "Anguilla"),
            new Origin("aq", "Antarctica"),
            new Origin("ag", "Antigua And Barbuda"),
            new Origin("ar", "Argentina"),
            new Origin("am", "Armenia"),
            new Origin("aw", "Aruba"),
            new Origin("au", "Australia"),
            new Origin("at", "Austria"),
            new Origin("az", "Azerbaijan"),
            new Origin("bs", "Bahamas"),
            new Origin("bh", "Bahrain"),
            new Origin("bd", "Bangladesh"),
            new Origin("bb", "Barbados"),
            new Origin("by", "Belarus"),
            new Origin("be", "Belgium"),
            new Origin("bz", "Belize"),
            new Origin("bj", "Benin"),
            new Origin("bm", "Bermuda"),
            new Origin("bt", "Bhutan"),
            new Origin("bo", "Bolivia"),
            new Origin("ba", "Bosnia And Herzegovina"),
            new Origin("bw", "Botswana"),
            new Origin("bv", "Bouvet Island"),
            new Origin("br", "Brazil"),
            new Origin("io", "British Indian Ocean Territory"),
            new Origin("bn", "Brunei Darussalam"),
            new Origin("bg", "Bulgaria"),
            new Origin("bf", "Burkina Faso"),
            new Origin("bi", "Burundi"),
            new Origin("kh", "Cambodia"),
            new Origin("cm", "Cameroon"),
            new Origin("ca", "Canada"),
            new Origin("cv", "Cape Verde"),
            new Origin("ky", "Cayman Islands"),
            new Origin("cf", "Central African Republic"),
            new Origin("td", "Chad"),
            new Origin("cl", "Chile"),
            new Origin("cn", "China"),
            new Origin("cx", "Christmas Island"),
            new Origin("cc", "Cocos (Keeling) Islands"),
            new Origin("co", "Colombia"),
            new Origin("km", "Comoros"),
            new Origin("cg", "Congo"),
            new Origin("cd", "Congo, The Democratic Republic Of The"),
            new Origin("ck", "Cook Islands"),
            new Origin("cr", "Costa Rica"),
            new Origin("ci", "Côte D'Ivoire"),
            new Origin("hr", "Croatia"),
            new Origin("cu", "Cuba"),
            new Origin("cy", "Cyprus"),
            new Origin("cz", "Czech Republic"),
            new Origin("dk", "Denmark"),
            new Origin("dj", "Djibouti"),
            new Origin("dm", "Dominica"),
            new Origin("do", "Dominican Republic"),
            new Origin("ec", "Ecuador"),
            new Origin("eg", "Egypt"),
            new Origin("sv", "El Salvador"),
            new Origin("gq", "Equatorial Guinea"),
            new Origin("er", "Eritrea"),
            new Origin("ee", "Estonia"),
            new Origin("et", "Ethiopia"),
            new Origin("fk", "Falkland Islands (Malvinas)"),
            new Origin("fo", "Faroe Islands"),
            new Origin("fj", "Fiji"),
            new Origin("fi", "Finland"),
            new Origin("fr", "France"),
            new Origin("gf", "French Guiana"),
            new Origin("pf", "French Polynesia"),
            new Origin("tf", "French Southern Territories"),
            new Origin("ga", "Gabon"),
            new Origin("gm", "Gambia"),
            new Origin("ge", "Georgia"),
            new Origin("de", "Germany"),
            new Origin("gh", "Ghana"),
            new Origin("gi", "Gibraltar"),
            new Origin("gr", "Greece"),
            new Origin("gl", "Greenland"),
            new Origin("gd", "Grenada"),
            new Origin("gp", "Guadeloupe"),
            new Origin("gu", "Guam"),
            new Origin("gt", "Guatemala"),
            new Origin("gn", "Guinea"),
            new Origin("gw", "Guinea-Bissau"),
            new Origin("gy", "Guyana"),
            new Origin("ht", "Haiti"),
            new Origin("hm", "Heard Island And Mcdonald Islands"),
            new Origin("va", "Holy See (Vatican City State)"),
            new Origin("hn", "Honduras"),
            new Origin("hk", "Hong Kong"),
            new Origin("hu", "Hungary"),
            new Origin("is", "Iceland"),
            new Origin("in", "India"),
            new Origin("id", "Indonesia"),
            new Origin("ir", "Iran, Islamic Republic Of"),
            new Origin("iq", "Iraq"),
            new Origin("ie", "Ireland"),
            new Origin("il", "Israel"),
            new Origin("it", "Italy"),
            new Origin("jm", "Jamaica"),
            new Origin("jp", "Japan"),
            new Origin("jo", "Jordan"),
            new Origin("kz", "Kazakhstan"),
            new Origin("ke", "Kenya"),
            new Origin("ki", "Kiribati"),
            new Origin("kp", "Korea, Democratic People'S Republic Of"),
            new Origin("kr", "Korea, Republic Of"),
            new Origin("kw", "Kuwait"),
            new Origin("kg", "Kyrgyzstan"),
            new Origin("la", "Lao People'S Democratic Republic"),
            new Origin("lv", "Latvia"),
            new Origin("lb", "Lebanon"),
            new Origin("ls", "Lesotho"),
            new Origin("lr", "Liberia"),
            new Origin("ly", "Libyan Arab Jamahiriya"),
            new Origin("li", "Liechtenstein"),
            new Origin("lt", "Lithuania"),
            new Origin("lu", "Luxembourg"),
            new Origin("mo", "Macao"),
            new Origin("mk", "Macedonia, The Former Yugoslav Republic Of"),
            new Origin("mg", "Madagascar"),
            new Origin("mw", "Malawi"),
            new Origin("my", "Malaysia"),
            new Origin("mv", "Maldives"),
            new Origin("ml", "Mali"),
            new Origin("mt", "Malta"),
            new Origin("mh", "Marshall Islands"),
            new Origin("mq", "Martinique"),
            new Origin("mr", "Mauritania"),
            new Origin("mu", "Mauritius"),
            new Origin("yt", "Mayotte"),
            new Origin("mx", "Mexico"),
            new Origin("fm", "Micronesia, Federated States Of"),
            new Origin("md", "Moldova, Republic Of"),
            new Origin("mc", "Monaco"),
            new Origin("mn", "Mongolia"),
            new Origin("ms", "Montserrat"),
            new Origin("ma", "Morocco"),
            new Origin("mz", "Mozambique"),
            new Origin("mm", "Myanmar"),
            new Origin("na", "Namibia"),
            new Origin("nr", "Nauru"),
            new Origin("np", "Nepal"),
            new Origin("nl", "Netherlands"),
            new Origin("an", "Netherlands Antilles"),
            new Origin("nc", "New Caledonia"),
            new Origin("nz", "New Zealand"),
            new Origin("ni", "Nicaragua"),
            new Origin("ne", "Niger"),
            new Origin("ng", "Nigeria"),
            new Origin("nu", "Niue"),
            new Origin("nf", "Norfolk Island"),
            new Origin("mp", "Northern Mariana Islands"),
            new Origin("no", "Norway"),
            new Origin("om", "Oman"),
            new Origin("pk", "Pakistan"),
            new Origin("pw", "Palau"),
            new Origin("ps", "Palestinian Territory, Occupied"),
            new Origin("pa", "Panama"),
            new Origin("pg", "Papua New Guinea"),
            new Origin("py", "Paraguay"),
            new Origin("pe", "Peru"),
            new Origin("ph", "Philippines"),
            new Origin("pn", "Pitcairn"),
            new Origin("pl", "Poland"),
            new Origin("pt", "Portugal"),
            new Origin("pr", "Puerto Rico"),
            new Origin("qa", "Qatar"),
            new Origin("re", "Réunion"),
            new Origin("ro", "Romania"),
            new Origin("ru", "Russian Federation"),
            new Origin("rw", "Rwanda"),
            new Origin("sh", "Saint Helena"),
            new Origin("kn", "Saint Kitts And Nevis"),
            new Origin("lc", "Saint Lucia"),
            new Origin("pm", "Saint Pierre And Miquelon"),
            new Origin("vc", "Saint Vincent And The Grenadines"),
            new Origin("ws", "Samoa"),
            new Origin("sm", "San Marino"),
            new Origin("st", "Sao Tome And Principe"),
            new Origin("sa", "Saudi Arabia"),
            new Origin("sn", "Senegal"),
            new Origin("cs", "Serbia And Montenegro"),
            new Origin("sc", "Seychelles"),
            new Origin("sl", "Sierra Leone"),
            new Origin("sg", "Singapore"),
            new Origin("sk", "Slovakia"),
            new Origin("si", "Slovenia"),
            new Origin("sb", "Solomon Islands"),
            new Origin("so", "Somalia"),
            new Origin("za", "South Africa"),
            new Origin("gs", "South Georgia And The South Sandwich Islands"),
            new Origin("es", "Spain"),
            new Origin("lk", "Sri Lanka"),
            new Origin("sd", "Sudan"),
            new Origin("sr", "Suriname"),
            new Origin("sj", "Svalbard And Jan Mayen"),
            new Origin("sz", "Swaziland"),
            new Origin("se", "Sweden"),
            new Origin("ch", "Switzerland"),
            new Origin("sy", "Syrian Arab Republic"),
            new Origin("tw", "Taiwan, Province Of China"),
            new Origin("tj", "Tajikistan"),
            new Origin("tz", "Tanzania, United Republic Of"),
            new Origin("th", "Thailand"),
            new Origin("tl", "Timor-Leste"),
            new Origin("tg", "Togo"),
            new Origin("tk", "Tokelau"),
            new Origin("to", "Tonga"),
            new Origin("tt", "Trinidad And Tobago"),
            new Origin("tn", "Tunisia"),
            new Origin("tr", "Turkey"),
            new Origin("tm", "Turkmenistan"),
            new Origin("tc", "Turks And Caicos Islands"),
            new Origin("tv", "Tuvalu"),
            new Origin("ug", "Uganda"),
            new Origin("ua", "Ukraine"),
            new Origin("ae", "United Arab Emirates"),
            new Origin("gb", "United Kingdom"),
            new Origin("us", "United States"),
            new Origin("um", "United States Minor Outlying Islands"),
            new Origin("uy", "Uruguay"),
            new Origin("uz", "Uzbekistan"),
            new Origin("vu", "Vanuatu"),
            new Origin("ve", "Venezuela"),
            new Origin("vn", "Viet Nam"),
            new Origin("vg", "Virgin Islands, British"),
            new Origin("vi", "Virgin Islands, U.S."),
            new Origin("wf", "Wallis And Futuna"),
            new Origin("eh", "Western Sahara"),
            new Origin("ye", "Yemen"),
            new Origin("zm", "Zambia"),
            new Origin("zw", "Zimbabwe")
        );
    }
}