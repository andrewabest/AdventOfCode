using System.Collections.Concurrent;
using Xunit;

namespace AdventOfCode2023;

public class Day5
{
    [Theory]
    [InlineData(0, 0, 1, 0, 0)]
    [InlineData(0, 0, 2, 1, 1)]
    public void GetOutputIsCorrect(long start, long end, long span, long input, long expectedOutput)
    {
        var range = new Range(start, end, span);
        var output = range.GetOutput(input);

        Assert.Equal(expectedOutput, output);
    }

    [Fact]
    void Main()
    {
        var map = GenerateMap();
        var inputs = new long[]
        {
            3121711159, 166491471, 3942191905, 154855415, 3423756552, 210503354, 2714499581, 312077252, 1371898531,
            165242002, 752983293, 93023991, 3321707304, 21275084, 949929163, 233055973, 3626585, 170407229, 395618482,
            226312891
        };

        var results = inputs.Select(i => map.GetResult(i));

        Assert.Equal(0, results.Min());
    }
    
    [Fact]
    void Main2()
    {
        var map = GenerateMap();
        var inputs = new (long, long)[]
        {
            (3121711159, 166491471), 
            (3942191905, 154855415), 
            (3423756552, 210503354), 
            (2714499581, 312077252), 
            (1371898531, 165242002), 
            (752983293, 93023991), 
            (3321707304, 21275084), 
            (949929163, 233055973), 
            (3626585, 170407229), 
            (395618482, 226312891)
        };

        var lowestPerStack = new ConcurrentBag<long>();
        
        Parallel.ForEach(inputs, new ParallelOptions { MaxDegreeOfParallelism = inputs.Length }, tuple =>
        {
            long lowest = long.MaxValue;
            for (long i = 0; i < tuple.Item2; i++)
            {
                var value = tuple.Item1 + i;
                var result = map.GetResult(value);
                if (result < lowest)
                {
                    lowest = result;
                }
            }

            lowestPerStack.Add(lowest);
        });

        Assert.Equal(0, lowestPerStack.Min());
    }

    public class Range(long outputStart, long inputStart, long span)
    {
        public bool HasInput(long input) => input >= inputStart && input < inputStart + span;

        public long GetOutput(long input)
        {
            if (!HasInput(input))
            {
                throw new ArgumentOutOfRangeException(nameof(input));
            }

            return outputStart + (input - inputStart);
        }
    }

    public class Map(Map? next)
    {
        private readonly List<Range> _ranges = new();

        public void AddRange(Range range) => _ranges.Add(range);

        public long GetResult(long input)
        {
            var range = _ranges.FirstOrDefault(r => r.HasInput(input));
            var output = range?.GetOutput(input) ?? input;

            return next?.GetResult(output) ?? output;
        }
    }

    static Map GenerateMap()
    {
        var humidityToLocation = new Map(null);
        humidityToLocation.AddRange(new Range(3693038281, 1946208152, 169064741));
        humidityToLocation.AddRange(new Range(3025397429, 1673895501, 272312651));
        humidityToLocation.AddRange(new Range(2522027478, 1111558812, 503369951));
        humidityToLocation.AddRange(new Range(3862103022, 3729735566, 432864274));
        humidityToLocation.AddRange(new Range(1111558812, 2115272893, 1374715990));
        humidityToLocation.AddRange(new Range(3356676818, 3489988883, 239746683));
        humidityToLocation.AddRange(new Range(3297710080, 1614928763, 58966738));
        humidityToLocation.AddRange(new Range(2486274802, 4162599840, 35752676));
        humidityToLocation.AddRange(new Range(3596423501, 4198352516, 96614780));

        var temperatureToHumitidy = new Map(humidityToLocation);
        temperatureToHumitidy.AddRange(new Range(1844491325, 2716144828, 118858329));
        temperatureToHumitidy.AddRange(new Range(1004942401, 2971501799, 229549152));
        temperatureToHumitidy.AddRange(new Range(696973964, 238546929, 19842755));
        temperatureToHumitidy.AddRange(new Range(716816719, 119302258, 119244671));
        temperatureToHumitidy.AddRange(new Range(444146335, 2339344684, 80152617));
        temperatureToHumitidy.AddRange(new Range(3752420807, 853580623, 112964826));
        temperatureToHumitidy.AddRange(new Range(3736479208, 2933101125, 15941599));
        temperatureToHumitidy.AddRange(new Range(1822411864, 805752927, 11014046));
        temperatureToHumitidy.AddRange(new Range(3183816206, 2835003157, 98097968));
        temperatureToHumitidy.AddRange(new Range(3538508914, 2576089466, 88076349));
        temperatureToHumitidy.AddRange(new Range(1833425910, 816766973, 11065415));
        temperatureToHumitidy.AddRange(new Range(3865385633, 2664165815, 51979013));
        temperatureToHumitidy.AddRange(new Range(1706894195, 1592117663, 89769434));
        temperatureToHumitidy.AddRange(new Range(2892814110, 3354360228, 291002096));
        temperatureToHumitidy.AddRange(new Range(2450334080, 3645362324, 65003481));
        temperatureToHumitidy.AddRange(new Range(3934652728, 3721018678, 66888233));
        temperatureToHumitidy.AddRange(new Range(3285923313, 2419497301, 85776839));
        temperatureToHumitidy.AddRange(new Range(3917364646, 2257358228, 17288082));
        temperatureToHumitidy.AddRange(new Range(3371700152, 2090549466, 166808762));
        temperatureToHumitidy.AddRange(new Range(524298952, 633077915, 172675012));
        temperatureToHumitidy.AddRange(new Range(1600852292, 966545449, 106041903));
        temperatureToHumitidy.AddRange(new Range(2283983708, 620036820, 13041095));
        temperatureToHumitidy.AddRange(new Range(2176983704, 2274646310, 64698374));
        temperatureToHumitidy.AddRange(new Range(2515337561, 1072587352, 32448957));
        temperatureToHumitidy.AddRange(new Range(846714263, 1105036309, 42935019));
        temperatureToHumitidy.AddRange(new Range(3719859664, 603417276, 16619544));
        temperatureToHumitidy.AddRange(new Range(2547786518, 258389684, 345027592));
        temperatureToHumitidy.AddRange(new Range(0, 1147971328, 444146335));
        temperatureToHumitidy.AddRange(new Range(1963349654, 3787906911, 213634050));
        temperatureToHumitidy.AddRange(new Range(836061390, 3710365805, 10652873));
        temperatureToHumitidy.AddRange(new Range(889649282, 0, 115293119));
        temperatureToHumitidy.AddRange(new Range(2241682078, 1681887097, 42301630));
        temperatureToHumitidy.AddRange(new Range(3281914174, 115293119, 4009139));
        temperatureToHumitidy.AddRange(new Range(2297024803, 3201050951, 153309277));
        temperatureToHumitidy.AddRange(new Range(1796663629, 827832388, 25748235));
        temperatureToHumitidy.AddRange(new Range(3626585263, 2505274140, 70815326));
        temperatureToHumitidy.AddRange(new Range(1234491553, 1724188727, 366360739));
        temperatureToHumitidy.AddRange(new Range(3697400589, 2949042724, 22459075));

        var lightToTemperature = new Map(temperatureToHumitidy);
        lightToTemperature.AddRange(new Range(3188351957, 4202865263, 58820659));
        lightToTemperature.AddRange(new Range(583430260, 717912118, 192120954));
        lightToTemperature.AddRange(new Range(1044551258, 2246397764, 71032709));
        lightToTemperature.AddRange(new Range(3109547837, 4261685922, 33281374));
        lightToTemperature.AddRange(new Range(1678878772, 1586709546, 87694921));
        lightToTemperature.AddRange(new Range(1115583967, 3604496785, 152969541));
        lightToTemperature.AddRange(new Range(3142829211, 2200875018, 45522746));
        lightToTemperature.AddRange(new Range(2103724421, 1412959073, 173750473));
        lightToTemperature.AddRange(new Range(3094755823, 2836912864, 14792014));
        lightToTemperature.AddRange(new Range(4233716778, 2851704878, 61250518));
        lightToTemperature.AddRange(new Range(1809783323, 3254776949, 293941098));
        lightToTemperature.AddRange(new Range(570222212, 2097755032, 13208048));
        lightToTemperature.AddRange(new Range(34744215, 693464, 72237295));
        lightToTemperature.AddRange(new Range(1773265141, 2502078476, 36518182));
        lightToTemperature.AddRange(new Range(775551214, 4072807084, 98261716));
        lightToTemperature.AddRange(new Range(2718819720, 511651563, 117721319));
        lightToTemperature.AddRange(new Range(873812930, 4171068800, 31796463));
        lightToTemperature.AddRange(new Range(905609393, 3933865219, 138941865));
        lightToTemperature.AddRange(new Range(373474927, 3892288779, 41576440));
        lightToTemperature.AddRange(new Range(3040218555, 3117948789, 54537268));
        lightToTemperature.AddRange(new Range(2397118702, 2538596658, 1887839));
        lightToTemperature.AddRange(new Range(0, 72930759, 34744215));
        lightToTemperature.AddRange(new Range(2277474894, 1316613368, 93303271));
        lightToTemperature.AddRange(new Range(370955311, 1409916639, 2519616));
        lightToTemperature.AddRange(new Range(1359408316, 3757466326, 134822453));
        lightToTemperature.AddRange(new Range(2370778165, 306806837, 1167210));
        lightToTemperature.AddRange(new Range(220512052, 1412436255, 522818));
        lightToTemperature.AddRange(new Range(3335711852, 1674404467, 423350565));
        lightToTemperature.AddRange(new Range(335173455, 2743397480, 35781856));
        lightToTemperature.AddRange(new Range(3900016435, 982913025, 333700343));
        lightToTemperature.AddRange(new Range(2399006541, 2540484497, 202912983));
        lightToTemperature.AddRange(new Range(3816795945, 2110963080, 83220490));
        lightToTemperature.AddRange(new Range(415051367, 910033072, 72879953));
        lightToTemperature.AddRange(new Range(2663040982, 3548718047, 55778738));
        lightToTemperature.AddRange(new Range(3759062417, 2779179336, 57733528));
        lightToTemperature.AddRange(new Range(3247172616, 629372882, 88539236));
        lightToTemperature.AddRange(new Range(1494230769, 2317430473, 184648003));
        lightToTemperature.AddRange(new Range(1766573693, 2194183570, 6691448));
        lightToTemperature.AddRange(new Range(2601919524, 245685379, 61121458));
        lightToTemperature.AddRange(new Range(2371945375, 220512052, 25173327));
        lightToTemperature.AddRange(new Range(221034870, 3003810204, 114138585));
        lightToTemperature.AddRange(new Range(2836541039, 307974047, 203677516));
        lightToTemperature.AddRange(new Range(487931320, 3172486057, 82290892));
        lightToTemperature.AddRange(new Range(106981510, 0, 693464));
        lightToTemperature.AddRange(new Range(1268553508, 2912955396, 90854808));

        var waterToLight = new Map(lightToTemperature);
        waterToLight.AddRange(new Range(66525849, 932008802, 34502691));
        waterToLight.AddRange(new Range(1231709999, 161981088, 108836128));
        waterToLight.AddRange(new Range(4050378444, 3046032039, 195065028));
        waterToLight.AddRange(new Range(1188304980, 324179540, 43405019));
        waterToLight.AddRange(new Range(0, 95455239, 66525849));
        waterToLight.AddRange(new Range(1134942656, 270817216, 53362324));
        waterToLight.AddRange(new Range(4015087939, 2401779423, 35290505));
        waterToLight.AddRange(new Range(3174436586, 2144628864, 257150559));
        waterToLight.AddRange(new Range(3688283374, 3968162731, 326804565));
        waterToLight.AddRange(new Range(101028540, 367584559, 564424243));
        waterToLight.AddRange(new Range(665452783, 23428765, 72026474));
        waterToLight.AddRange(new Range(2144628864, 2437069928, 302742058));
        waterToLight.AddRange(new Range(1340546127, 0, 23428765));
        waterToLight.AddRange(new Range(737479257, 1714174561, 397463399));
        waterToLight.AddRange(new Range(3431587145, 2789335810, 256696229));
        waterToLight.AddRange(new Range(4245443472, 2739811986, 49523824));
        waterToLight.AddRange(new Range(1363974892, 966511493, 747663068));
        waterToLight.AddRange(new Range(2447370922, 3241097067, 727065664));

        var fertilizerToWater = new Map(waterToLight);
        fertilizerToWater.AddRange(new Range(2485494684, 3430237839, 78539769));
        fertilizerToWater.AddRange(new Range(2045426403, 2253341567, 99573285));
        fertilizerToWater.AddRange(new Range(290571869, 0, 280695139));
        fertilizerToWater.AddRange(new Range(3540352207, 2045426403, 63912525));
        fertilizerToWater.AddRange(new Range(2879366909, 3356847577, 67075608));
        fertilizerToWater.AddRange(new Range(868611408, 858081124, 224160766));
        fertilizerToWater.AddRange(new Range(2304858397, 2525185867, 55003581));
        fertilizerToWater.AddRange(new Range(189640733, 280695139, 100931136));
        fertilizerToWater.AddRange(new Range(2144999688, 3983682880, 159858709));
        fertilizerToWater.AddRange(new Range(3374818325, 3858616265, 14108175));
        fertilizerToWater.AddRange(new Range(3604264732, 2580189448, 427645906));
        fertilizerToWater.AddRange(new Range(1730244179, 535856806, 2894582));
        fertilizerToWater.AddRange(new Range(4242091162, 3634410314, 52876134));
        fertilizerToWater.AddRange(new Range(4031910638, 3872724440, 110958440));
        fertilizerToWater.AddRange(new Range(1092772174, 1116784935, 90115688));
        fertilizerToWater.AddRange(new Range(1182887862, 381626275, 154230531));
        fertilizerToWater.AddRange(new Range(4149183732, 3263940147, 92907430));
        fertilizerToWater.AddRange(new Range(4142869078, 3423923185, 6314654));
        fertilizerToWater.AddRange(new Range(571267008, 728392121, 129689003));
        fertilizerToWater.AddRange(new Range(3315918322, 2352914852, 58900003));
        fertilizerToWater.AddRange(new Range(2359861978, 3508777608, 125632706));
        fertilizerToWater.AddRange(new Range(2735364270, 2109338928, 144002639));
        fertilizerToWater.AddRange(new Range(2946442517, 2411814855, 113371012));
        fertilizerToWater.AddRange(new Range(1733138761, 1082241890, 34543045));
        fertilizerToWater.AddRange(new Range(700956011, 1600026409, 167655397));
        fertilizerToWater.AddRange(new Range(3059813529, 3041876971, 222063176));
        fertilizerToWater.AddRange(new Range(2564034453, 3687286448, 171329817));
        fertilizerToWater.AddRange(new Range(3281876705, 3007835354, 34041617));
        fertilizerToWater.AddRange(new Range(0, 538751388, 189640733));
        fertilizerToWater.AddRange(new Range(3388926500, 4143541589, 151425707));
        fertilizerToWater.AddRange(new Range(1337118393, 1206900623, 393125786));

        var soilToFertilizer = new Map(fertilizerToWater);
        soilToFertilizer.AddRange(new Range(69994133, 1665188283, 300635345));
        soilToFertilizer.AddRange(new Range(0, 1965823628, 36826481));
        soilToFertilizer.AddRange(new Range(2222587532, 2553838094, 476943506));
        soilToFertilizer.AddRange(new Range(2929387922, 3030781600, 856348250));
        soilToFertilizer.AddRange(new Range(4182440411, 2441311209, 112526885));
        soilToFertilizer.AddRange(new Range(36826481, 2002650109, 33167652));
        soilToFertilizer.AddRange(new Range(2044606970, 4116986734, 177980562));
        soilToFertilizer.AddRange(new Range(1815516279, 549395220, 220301482));
        soilToFertilizer.AddRange(new Range(1186435707, 0, 549395220));
        soilToFertilizer.AddRange(new Range(916609638, 1315676862, 269826069));
        soilToFertilizer.AddRange(new Range(3785736172, 2044606970, 396704239));
        soilToFertilizer.AddRange(new Range(2699531038, 3887129850, 229856884));
        soilToFertilizer.AddRange(new Range(1735830927, 1585502931, 79685352));
        soilToFertilizer.AddRange(new Range(370629478, 769696702, 545980160));

        var seedToSoil = new Map(soilToFertilizer);
        seedToSoil.AddRange(new Range(522866878, 679694818, 556344137));
        seedToSoil.AddRange(new Range(1206934522, 1236038955, 57448427));
        seedToSoil.AddRange(new Range(2572695651, 3529213882, 270580892));
        seedToSoil.AddRange(new Range(1082547209, 29063229, 124387313));
        seedToSoil.AddRange(new Range(2080101996, 2392534586, 180161065));
        seedToSoil.AddRange(new Range(1079211015, 153450542, 3336194));
        seedToSoil.AddRange(new Range(2466695431, 2286534366, 106000220));
        seedToSoil.AddRange(new Range(1887791814, 2094224184, 192310182));
        seedToSoil.AddRange(new Range(2843276543, 2572695651, 956518231));
        seedToSoil.AddRange(new Range(2341707296, 1887791814, 124988135));
        seedToSoil.AddRange(new Range(1264382949, 156786736, 473875833));
        seedToSoil.AddRange(new Range(67220304, 1331644457, 455646574));
        seedToSoil.AddRange(new Range(3903217571, 3799794774, 267521683));
        seedToSoil.AddRange(new Range(2260263061, 2012779949, 81444235));
        seedToSoil.AddRange(new Range(1738258782, 630662569, 49032249));
        seedToSoil.AddRange(new Range(29063229, 1293487382, 38157075));
        seedToSoil.AddRange(new Range(3799794774, 4067316457, 103422797));

        return seedToSoil;
    }
}