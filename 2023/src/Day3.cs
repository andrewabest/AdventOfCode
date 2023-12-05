﻿using Xunit;

namespace AdventOfCode2023;

public class Day3
{
    [Fact]
    public void Works()
    {
        Assert.Equal(445234, Main(actualInput));
    }
    
    [Theory]
    [InlineData(".....", ".123.", ".....", "123", 1)]
    public void IsPartNumber_DoesNotReturnTrueIfNoSymbolPresent(string previous, string current, string next, string candidate, int candidateIndex)
    {
        Assert.False(IsPartNumber(previous, current, next, candidate, candidateIndex));
    }

    [Theory]
    [InlineData("$....", ".123.", ".....", "123", 1)]
    [InlineData(".....", "$123.", ".....", "123", 1)]
    [InlineData(".....", ".123.", "$....", "123", 1)]
    public void IsPartNumber_ParsesLeftBoundaryCorrectly(string previous, string current, string next, string candidate, int candidateIndex)
    {
        Assert.True(IsPartNumber(previous, current, next, candidate, candidateIndex));
    }
    
    [Theory]
    [InlineData(".....", ".123.", "....$", "123", 1)]
    [InlineData(".....", ".123$", ".....", "123", 1)]
    [InlineData("....$", ".123.", ".....", "123", 1)]
    public void IsPartNumber_ParsesRightBoundaryCorrectly(string previous, string current, string next, string candidate, int candidateIndex)
    {
        Assert.True(IsPartNumber(previous, current, next, candidate, candidateIndex));
    }
    
    [Theory]
    [InlineData("$....", ".123.", ".....", "123", 1)]
    [InlineData(".$...", ".123.", ".....", "123", 1)]
    [InlineData("..$..", ".123.", ".....", "123", 1)]
    [InlineData("...$.", ".123.", ".....", "123", 1)]
    [InlineData("....$", ".123.", ".....", "123", 1)]
    public void IsPartNumber_ParsesPreviousBoundaryCorrectly(string previous, string current, string next, string candidate, int candidateIndex)
    {
        Assert.True(IsPartNumber(previous, current, next, candidate, candidateIndex));
    }
    
    [Theory]
    [InlineData(".....", ".123.", "$....", "123", 1)]
    [InlineData(".....", ".123.", ".$...", "123", 1)]
    [InlineData(".....", ".123.", "..$..", "123", 1)]
    [InlineData(".....", ".123.", "...$.", "123", 1)]
    [InlineData(".....", ".123.", "....$", "123", 1)]
    public void IsPartNumber_ParsesNextBoundaryCorrectly(string previous, string current, string next, string candidate, int candidateIndex)
    {
        Assert.True(IsPartNumber(previous, current, next, candidate, candidateIndex));
    }

    readonly char[] _symbols = { '*', '&', '$', '/', '=', '%', '#', '-', '+' };

    bool IsPartNumber(string previous, string current, string next, string candidate, int candidateIndex)
    {
        var hasLeft = candidateIndex > 0;
        var hasRight = candidateIndex + candidate.Length < current.Length;
        var hasPrevious = !string.IsNullOrWhiteSpace(previous);
        var hasNext = !string.IsNullOrWhiteSpace(next);

        var candidates = new List<char>();
        // Get our left-hand-side candidates
        if (hasLeft)
        {
            if (hasPrevious) candidates.Add(previous[candidateIndex - 1]);
            candidates.Add(current[candidateIndex - 1]);
            if (hasNext) candidates.Add(next[candidateIndex - 1]);
        }

        // Get our right hand side candidates
        if (hasRight)
        {
            if (hasPrevious) candidates.Add(previous[candidateIndex + candidate.Length]);
            candidates.Add(current[candidateIndex + candidate.Length]);
            if (hasNext) candidates.Add(next[candidateIndex + candidate.Length]);
        }

        // Get the top and bottom bordering candidates
        if (hasPrevious) candidates.AddRange(previous[candidateIndex..(candidateIndex + candidate.Length)]);
        if (hasNext) candidates.AddRange(next[candidateIndex..(candidateIndex + candidate.Length)]);

        return candidates.Any(_symbols.Contains);
    }
    
    int Main(string input)
    {
        var lines = input.Split(Environment.NewLine);

        

        var result = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var candidate = string.Empty;
            for (int j = 0; j < line.Length; j++)
            {
                var c = line[j];

                if (char.IsDigit(c))
                {
                    candidate = $"{candidate}{c}";
                }

                // If we are reading a dot, or have reached the end of the line, and we have a candidate, evaluate it.
                if ((c == '.' || j == line.Length - 1) && !string.IsNullOrEmpty(candidate))
                {
                    if (IsPartNumber(
                            i > 0 ? lines[i - 1] : string.Empty,
                            line,
                            i < lines.Length - 1 ? lines[i + 1] : string.Empty,
                            candidate,
                            j - candidate.Length))
                    {
                        result += Int32.Parse(candidate);
                    }

                    candidate = string.Empty;
                }
            }
        }

        return result;
    }


// The engine schematic (your puzzle input) consists of a visual representation of the engine. 
// There are lots of numbers and symbols you don't really understand, 
// but apparently any number adjacent to a symbol, even diagonally, is a "part number" and should be included in your sum. 
// (Periods (.) do not count as a symbol.)

    const string actualInput = @"
....411...............838......721.....44..............................................607..................................................
...&......519..................*..........#.97.........994..............404..............*...&43........440...882.......673.505.............
.....*......*...892.........971...%....131....*..........*.......515...$.......157.....412.............-.....*.............*............594.
..856.495....13...-...............602..........36...$.985....341*.........88.....*.921....................122..................806..508.....
......................667.325*734.................718...............284..*....288..*....620.......854.............643....817....*...........
....*480..825..........*....................784.......&.............*...859........856.*..........*...........................137...........
............*..903*986.681....403.....451...*.....424..24.855....844....................826.......202....-.........542%..564......@.212*....
......735...70.............=.....*895.......575.....*......*.............490.........+.......114.......890....519...........*857.88.....761.
.......%..........#.#.....896.....................600.821@..565.............*390..664........@...292@..................%....................
....$...........256..340.........851..............................476..................................210..............102.................
....758..........................*......=.............@................................273......911...#....@666...+193......................
.............604....483..&144.859......807...-.........995..-218.770............37.512.*.........*.........................215...........117
......354..........*...............$........849.*.................................*.....242....469.&764.........................959*128.$...
.........&........575..521#..30..812..89.........836....../..........116............385....................942$..739....*46.................
..912*.......*845..........................................338..165.#....995.160......*....882.......421..........-..697.......619..........
......154..........................700...........................*....$.........*710..923....=.........*....................................
...................616..328........*......184..........227..401.635...264.=16....................-.....659....................613.....526...
.............33.......+.-........63..........#..........*..*.....................@............647..................352/.........*......*....
...107.......+............=318.................&862..210....588...703.........754.......151...................747.......260.....201..458....
...*..................................&671.........................................473......812..391.........&......249.....................
....13...................709.21..876....................-..52.........965.527*814...................*...*63...........*...75.400.....*619...
.............713.-...319*......$......................215.*.............*................@........407.67..............802.+.....*.466.......
.............*...230.............997......................80.526..3.811.541..753.........642......................212........119........+738
....197.....6......................=..618.....39.364.........+....*./........*................217*......443*703......*......................
......*.................240...........#......*....*............378............156.................763.............444....94...86......../273
.....866..&.........273*..................970.....673.....&........*359.............*565..147.............238...........+....&..............
.........690.761...........*985.......%...................691...587........../...663..................%....$......34................336.....
.......#...../....951+...75........708.......411..939..85.............189...143....................301..-......=..@......-......596.........
......406.....................+............+.........*...*..55.974%.......................203..179.......917..399.........451...............
....................177......220...........500......111.46.........................413....*.....*................................/..........
500............+....../.167-....................959.............231..................=..192...........334.....781.....&..........122..151...
...*............969..........+878..136.../........*.#.............*.&...........226......................*...*......855..296..........%.....
....421...611............448.........*....490..631...16........200..328........*..................51..272.....525..........*................
............@........@......=.....719.....................................57..46.............................................*844.....305...
.....868..............887.....@...........-..........@........964.........*..........-859.................#...............189..........@....
.....*.......804...............132..688....420.740.868.609......*....849...70..972.........991.....799.....43......574@.....................
935...99.530....*......................@.........................243..*............675.345*..........#.............................@........
...........*.724...........645....114$....364......154.....989/......440.............=..........................275.......594......788......
........756................................*.....................+........................187...504............./...../......*832...........
374................*.......586.......656..239....................40.........................*....*.......334*.........580...................
...*..............780..715......680...*..............490*...+715......648..........875.....735....359.......................................
563........662...................*..260.....896.357....................#.......407*....................................520.382.59.491.......
......204.*....................947.........../..*..............%.........................506.../..#..269#..*...........*.........*..........
.......*..150............355...................371...430.....155.107...............322....+..137.303......424..772......33..................
778...871.......*769.290*..........&..............................*................./.........................*...................743.153...
.............140............938..763..........3...............443..510....868...579.....577/....264............572.........#........*...$...
........*238...................#......417....=.......&....337..&.........#...........*.......................$.............125......153.....
.970$...........695..57..540........%..*..........930........*.............754%......156...560*305..567..78..149.974........................
..........@...........+...*........891.531....848......105....911....................................*...*........*.....825.................
..........573..525..&....958.777...............*..........*...........883....=....%.........158.....530..839....803............254*688......
..................*..560.............&......592.........159......743.......209.....735......*.......................237.....................
................15..................54.665........858..............*....93...................438.239*825.......284.....*.....565*684........
......732..............574.690..........@.....471....*....@.......157....*............692.....................*........495..................
...$.*.....320..9..........@......*576..........*........196..........738.................47......%955...80...983..505...............249....
.468.541.........%.............134...............974..............=................424........533..........*..........*...727...............
..........$.................51...............193.....824....#....170...667*107.200..*...380...+........=.302.440#....791...-.........394@...
....*....331.+662....540*............445......*..........27..742................*..972....*..........255......................762...........
...770...........................301*....947.673.....164............../353...228.......237...................&246....431%.942*........21....
........................................................*809..79..................#178.......409...@.....263......................285.......
114.................233.894......998....663.....65.830..........*...457............................598......*............554.....-..........
.......740......204....*.........*.....=..........*....655...803.......................................&....957...........*.........514.....
...151*...........................438.......*208..........@................644.....294..531........58...241.....&949....612...511..%........
..................534@........=.......*..494........569.......#........245.....980..*......*.......*.........................-..............
....$267.972............@......308...366...........*...../.....883....*.....-.../..784.....954.232.224.........121..................293.....
..........@..........409...........................487....540......699.....756.................*.........437-.=.......@.......#......@......
....957.........471.........795.#514.268.....443........................................=532..217....................171.144...246..........
....*....59.642...@...........=......*...694...................*..........594..308..................933.........39........=...........*.....
.....393..*....%.........479.......305..*...................593.789.........*.....*.......78.@211.....&.....................866......116....
.531..................%.../....911.....763..453..291.................+684..917..93....365..*.......48.........*834...545...#................
....*280....-919...365.............115.........*....=.......................................213...../......120..........*............+......
803.......................788........&.........764.....................*895.315........402......................824.....949....679..243.....
..........176.............*...............908$..................882.859...................*....586.......#404.-...=............-............
..366.......*..37*840.....66.........189............*....368*.....-.....391......803.....512.......478........294.....................978...
...........977.............................388....932........759........*...........*........619...*......845.....78.247........441.........
.........%.......................613....................355...........632.........................270.....*.......*.....*691.....%......$327
.......797..........................*.........473..451.....*..846.@................474*.......@.......-....561..493.........................
235.....................................880#...-...%....951.....*.490.507..............430....435.....370.............540..17.984..107......
.....280..986...589.367......595.............................110........$.....325...................................+../..*...%......#......
..........*...../......*........*949.......469.....#590.@......................*................912.......-.......269....818................
..697.................106..315........./.................366..................795....614*800......%./666...396.........................798..
....*.122.645....50.......*..........67..186.397.591.-.........353....201....................944............................&798......=.....
..193.+....#.....*.......918...............*....*.....930.......*......*....41..388.....387...+......&.......869.505........................
.........$....595............709........763.......365............649..705..........=....*......./.....32.....*.....*............609.404*....
.......540...........189.......*..442.........623*......$...............................295..524............64.....436......426*........15..
...........129...466.*.......513...*..................175.........824...820..........................460.......154..................962.....
.................*......306=......307......=...-...........*........*......%....$............./344./.....811....$........817..........*.....
.........$..923.118...........343........811.787.........329.......650.179....180..................648...%................=......157..656...
........186.$.........995......#.........................................*...............-.....524..........................................
..................682...%.........-...+...............411................20...659.........635.....$....*768..#.......986..269...............
........=.....143.#...........@.118.769..134...........#..=....332#...........................107...140.......569............*.930&.........
........92...$..............471...........*........485.....961..............15.............46*.............................820......838.649.
...................353..............865....991.629....*................841...*........@....................../129........#.............*....
......163.716.......*....123.892....*..............#.727..611.........*......236.498.36..434..762.........................438...5...........
...............903.688..*....../..378............662.....#.....934....1..........-.........+.*....110.88........124.....................513.
....550...299...........430.............*638.684...............*..........157................220.....*................751......336..647*....
....*...../..........................946...........873*203.337.4.............*......553..........921....553.....743...............*.........
....975.....*76................867............103............%.....603.....588.672*...#......728..*.....%......$......877...332*...903.+850.
.........542....592....992.......*.............+.....801..........................................215.................%.....................
.491...........*........*.................545.........*.....@..............33...233...782.....417..........%415.............%.........*119..
.................../929..123........275.........409..494.251...............*.......*.....*830...*....709.........368......932......626......
..47.837....926........................@.=864.....*.................482............972.........545..%....424......./..........990...........
......*.......*.../354..........$................107.......377+.......-....................................*.................*....50*.......
....351....177.........801....789...871......190..................&.........-408.292.....243..884........419.@936.795.......258......227....
...............323+.....*.............-..508*.........@......$396..817............*..431....*.......*678.................................288
...........*........+15...../..................=....144................*510.....177.....*....403.524.....&58.735....#........519..615%......
........574.328..@........362..201*109..........816.................379...............84.........................312...........*.......212..
................411....................550.................875.#.........172.....&...................133.799..................849..274*.....
.....................890*942..........*................264.....361.444........189....817..812....333...*.*..................................
...+430...............................................*............*......490.............*..........37...160..166.............%......*.....
........532..............743&...595...217.............260...........740......*..4..453..89..%..304.............*...+........769........793..
.........+..560..702=..............*..*......270..........-.527.717........675..=..........954...=......=398..9..916..............323.......
....835.......*..........%...144..959........@.........419.........*251...............................*..................360..586*.....273..
...............766.....113....*..................................#............68...............%...944.....390%...418.......%...........*...
...................467.......548.....922.......125...792......44..65.....263*.....26....369...688.......................757...216*637..292..
.....*309..347....*...................*...........*.....*.......-..................%...*.............815.571.............*..................
..786.............473...807=..164......781......959......27..........28*.........................687*.....*.............259..............939
....../........................&...333........................876.....................132..............=..396.....742+.............281......
...210.....@............899........*......%.....873..24*167..*.....664.................%..465.........633................492.........*......
...........639.........*........464......738......*..........766...&......396.775..151.......*375........./177..........*.....245.752.......
.501.................@..43.527.................415...782...................*..*....*................*537........=331..37....................
.......93.........723........*....882...............@.....186&......642..150.593...414.......117.718........851.............................
...#.....*..................275.....#.503...23=.................145..*........................*.......645*.....+................33.310+..603
537.........$..........#164...........&...........887.......562*......497....742............97...602......290...........................@...
....../375...845....................................*.............510.............315*720......................./.......69*114.....968......
...................153.662........276...795........356...............&...365..................................333....................*......
.995.980..............*...........*....*................375.............$.......500............148...................*915........$..........
...*.-........32............243...258.674...........+..#........................*...531....590*...........837.408.100..........609..296.....
.532............*.690..734.....*...................828..........#............502...*.........................*..........&..........*........
.............215.....*...*.....800...@.......690..........79....403..785..........363...................159..........383..322*99...561...867
...384.613...............565..........106.....&.............%..........*..215.........-.....55*758.516.....#............................#...
...../..*...986..539.425.........................682@.........719...880..*......135...119............................686.........988........
.......373.......*.....*...508..862.................../862...*..........647......*..............638..............204...*..............808...
................284...968....@....*..........382.438$.......561................709.59..............#...509.......*......234.......235*......
........432.....................348...................714..........................%.....990.............*.....581..543......844............
..580/.....*...............41...........384....243...*....................=...389*..........*140..-......683..........*.....................
...................73.*242..+.............*........323..78........700..978........761.....%.......626...............93................@.....
.567........945....*............199..........601*.........*........*...................800..365.............982.897..................962....
.....1......*...$..468............*..............154.......626....662........606............*........337..@......*....121...707..945........
..........568..818.............813..424@.............*642.............2......*...589.....678....963....*.342......162...=..=........#.......
......114...............81........................767......................720......................260.....................................";
}