// Skipping function Leaderboard(), it contains poisonous unsupported syntaxes

func @_Public_Bot.Modules.Commands.LevelCommands.RankBuilder.MakeRank$string.string.int.int.int.System.Drawing.Color.int.System.Drawing.Color.string$(none, none, i32, i32, i32, none, i32, none, none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :12) {
^entry (%_username : none, %_avtr : none, %_level : i32, %_curXP : i32, %_nxtXP : i32, %_embc : none, %_Rank : i32, %_bgc : none, %_bkurl : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :56)
cbde.store %_username, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :56)
%1 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :73)
cbde.store %_avtr, %1 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :73)
%2 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :86)
cbde.store %_level, %2 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :86)
%3 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :97)
cbde.store %_curXP, %3 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :97)
%4 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :108)
cbde.store %_nxtXP, %4 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :108)
%5 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :119)
cbde.store %_embc, %5 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :119)
%6 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :146)
cbde.store %_Rank, %6 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :146)
%7 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :156)
cbde.store %_bgc, %7 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :156)
%8 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :182)
cbde.store %_bkurl, %8 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :53 :182)
br ^0

^0: // BinaryBranchBlock
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :55 :31) // new WebClient() (ObjectCreationExpression)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :56 :31) // Not a variable of known type: wc
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :56 :47) // Not a variable of known type: avtr
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :56 :31) // wc.DownloadData(avtr) (InvocationExpression)
%15 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :57 :51) // Not a variable of known type: bytes
%16 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :57 :34) // new MemoryStream(bytes) (ObjectCreationExpression)
// Entity from another assembly: System
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :58 :43) // System.Drawing.Image (SimpleMemberAccessExpression)
%19 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :58 :75) // Not a variable of known type: ms
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :58 :43) // System.Drawing.Image.FromStream(ms) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: ResizeImage
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :59 :34) // Not a variable of known type: pfp
%23 = constant 200 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :59 :39)
%24 = constant 200 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :59 :44)
%25 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :59 :22) // ResizeImage(pfp, 200, 200) (InvocationExpression)
%26 = constant 913 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :60 :38)
%27 = constant 312 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :60 :43)
%28 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :60 :27) // new Bitmap(913, 312) (ObjectCreationExpression)
// Entity from another assembly: Graphics
%30 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :61 :46) // Not a variable of known type: btmp
%31 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :61 :27) // Graphics.FromImage(btmp) (InvocationExpression)
%33 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :62 :16) // Not a variable of known type: canv
%34 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :62 :16) // canv.SmoothingMode (SimpleMemberAccessExpression)
// Entity from another assembly: SmoothingMode
%35 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :62 :37) // SmoothingMode.AntiAlias (SimpleMemberAccessExpression)
%36 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :63 :20) // Not a variable of known type: bkurl
%37 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :63 :29) // null (NullLiteralExpression)
%38 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :63 :20) // comparison of unknown type: bkurl == null
cond_br %38, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :63 :20)

^1: // SimpleBlock
%39 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :20) // Not a variable of known type: canv
%40 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :49) // Not a variable of known type: bgc
%41 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :34) // new SolidBrush(bgc) (ObjectCreationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: RoundedRect
%42 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :81)
%43 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :84)
%44 = constant 913 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :87)
%45 = constant 312 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :92)
%46 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :67) // new Rectangle(0, 0, 913, 312) (ObjectCreationExpression)
%47 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :98)
%48 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :55) // RoundedRect(new Rectangle(0, 0, 913, 312), 30) (InvocationExpression)
%49 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :64 :20) // canv.FillPath(new SolidBrush(bgc), RoundedRect(new Rectangle(0, 0, 913, 312), 30)) (InvocationExpression)
br ^3

^2: // SimpleBlock
%50 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :67 :33) // Not a variable of known type: wc
%51 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :67 :49) // Not a variable of known type: bkurl
%52 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :67 :33) // wc.DownloadData(bkurl) (InvocationExpression)
%54 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :68 :57) // Not a variable of known type: btz
%55 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :68 :40) // new MemoryStream(btz) (ObjectCreationExpression)
// Entity from another assembly: System
%57 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :69 :49) // System.Drawing.Image (SimpleMemberAccessExpression)
%58 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :69 :81) // Not a variable of known type: mems
%59 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :69 :49) // System.Drawing.Image.FromStream(mems) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: ResizeImage
%61 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :70 :42) // Not a variable of known type: bannr
%62 = constant 913 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :70 :49)
%63 = constant 312 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :70 :54)
%64 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :70 :30) // ResizeImage(bannr, 913, 312) (InvocationExpression)
%66 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :20) // Not a variable of known type: canv
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GeneralCommands
%67 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :35) // GeneralCommands.GuildStatBuilder (SimpleMemberAccessExpression)
%68 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :81) // Not a variable of known type: fin
%69 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :86)
%70 = constant 913 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :90)
%71 = constant 312 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :95)
%72 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :35) // GeneralCommands.GuildStatBuilder.RoundCorners(fin, 30, 913, 312) (InvocationExpression)
%73 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :111)
%74 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :114)
%75 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :101) // new Point(0, 0) (ObjectCreationExpression)
%76 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :71 :20) // canv.DrawImage(GeneralCommands.GuildStatBuilder.RoundCorners(fin, 30, 913, 312), new Point(0, 0)) (InvocationExpression)
br ^3

^3: // BinaryBranchBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: ClipToCircle
%77 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :40) // Not a variable of known type: pfp
%78 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :56) // Not a variable of known type: pfp
%79 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :56) // pfp.Width (SimpleMemberAccessExpression)
%80 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :68)
%81 = divis %79, %80 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :56)
%82 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :71) // Not a variable of known type: pfp
%83 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :71) // pfp.Height (SimpleMemberAccessExpression)
%84 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :84)
%85 = divis %83, %84 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :71)
%86 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :45) // new PointF(pfp.Width / 2, pfp.Height / 2) (ObjectCreationExpression)
%87 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :88) // Not a variable of known type: pfp
%88 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :88) // pfp.Width (SimpleMemberAccessExpression)
%89 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :100)
%90 = divis %88, %89 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :88)
// Entity from another assembly: System
%91 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :103) // System.Drawing.Color (SimpleMemberAccessExpression)
%92 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :103) // System.Drawing.Color.Transparent (SimpleMemberAccessExpression)
%93 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :74 :27) // ClipToCircle(pfp, new PointF(pfp.Width / 2, pfp.Height / 2), pfp.Width / 2, System.Drawing.Color.Transparent) (InvocationExpression)
%95 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :75 :16) // Not a variable of known type: canv
%96 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :75 :31) // Not a variable of known type: rpfp
%97 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :75 :37)
%98 = constant 25 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :75 :41)
%99 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :75 :16) // canv.DrawImage(rpfp, 40, 25) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: RoundedRect
%100 = constant 20 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :50)
%101 = constant 312 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :54)
%102 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :60)
%103 = subi %101, %102 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :54)
%104 = constant 913 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :70)
%105 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :76)
%106 = subi %104, %105 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :70)
%107 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :64) // (int)(913 - 60) (CastExpression)
%108 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :81)
%109 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :36) // new Rectangle(20, 312 - 60, (int)(913 - 60), 30) (ObjectCreationExpression)
%110 = constant 15 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :86)
%111 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :77 :24) // RoundedRect(new Rectangle(20, 312 - 60, (int)(913 - 60), 30), 15) (InvocationExpression)
%113 = constant 913 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :79 :36)
%114 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :79 :42)
%115 = subi %113, %114 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :79 :36)
%116 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :79 :30) // (int)(913 - 60) (CastExpression)
%117 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :79 :20) // mxWidth
cbde.store %116, %117 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :79 :20)
%118 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :80 :30)
%119 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :80 :20) // mnWidth
cbde.store %118, %119 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :80 :20)
%120 = cbde.load %3 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :81 :34)
%121 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :81 :26) // (double)curXP (CastExpression)
%122 = cbde.load %4 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :81 :50)
%123 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :81 :42) // (double)nxtXP (CastExpression)
%124 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :81 :26) // Binary expression on unsupported types (double)curXP / (double)nxtXP
// Entity from another assembly: Math
%126 = cbde.load %117 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :82 :39)
%127 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :82 :49) // Not a variable of known type: prc
%128 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :82 :39) // Binary expression on unsupported types mxWidth * prc
%129 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :82 :26) // Math.Ceiling(mxWidth * prc) (InvocationExpression)
%131 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :83 :20) // Not a variable of known type: fnl
%132 = cbde.load %119 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :83 :26)
%133 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :83 :20) // comparison of unknown type: fnl < mnWidth
cond_br %133, ^4, ^5 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :83 :20)

^4: // SimpleBlock
%134 = cbde.load %119 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :84 :26)
br ^5

^5: // BinaryBranchBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: RoundedRect
%135 = constant 20 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :52)
%136 = constant 312 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :56)
%137 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :62)
%138 = subi %136, %137 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :56)
%139 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :71) // Not a variable of known type: fnl
%140 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :66) // (int)fnl (CastExpression)
%141 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :76)
%142 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :38) // new Rectangle(20, 312 - 60, (int)fnl, 30) (ObjectCreationExpression)
%143 = constant 15 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :81)
%144 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :85 :26) // RoundedRect(new Rectangle(20, 312 - 60, (int)fnl, 30), 15) (InvocationExpression)
%146 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :16) // Not a variable of known type: canv
// Entity from another assembly: System
%147 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :45) // System.Drawing.Color (SimpleMemberAccessExpression)
%148 = constant 50 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :75)
%149 = constant 50 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :79)
%150 = constant 50 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :83)
%151 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :45) // System.Drawing.Color.FromArgb(50, 50, 50) (InvocationExpression)
%152 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :30) // new SolidBrush(System.Drawing.Color.FromArgb(50, 50, 50)) (ObjectCreationExpression)
%153 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :89) // Not a variable of known type: g
%154 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :86 :16) // canv.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(50, 50, 50)), g) (InvocationExpression)
%155 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :87 :16) // Not a variable of known type: canv
%156 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :87 :45) // Not a variable of known type: embc
%157 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :87 :30) // new SolidBrush(embc) (ObjectCreationExpression)
%158 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :87 :52) // Not a variable of known type: prg
%159 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :87 :16) // canv.FillPath(new SolidBrush(embc), prg) (InvocationExpression)
%160 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :90 :29) // Not a variable of known type: username
%161 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :90 :44) // '#' (CharacterLiteralExpression)
%162 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :90 :29) // username.Split('#') (InvocationExpression)
%164 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :26) // string (PredefinedType)
%165 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :38) // "#" (StringLiteralExpression)
%166 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :43) // Not a variable of known type: usrnam
%167 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :55) // Not a variable of known type: usrnam
%168 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :55) // usrnam.Length (SimpleMemberAccessExpression)
%169 = constant 1 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :71)
%170 = subi %168, %169 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :55)
%171 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :43) // usrnam.Take(usrnam.Length - 1) (InvocationExpression)
%172 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :91 :26) // string.Join("#", usrnam.Take(usrnam.Length - 1)) (InvocationExpression)
%174 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :26) // "#" (StringLiteralExpression)
%175 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :32) // Not a variable of known type: usrnam
%176 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :39) // Not a variable of known type: usrnam
%177 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :39) // usrnam.Length (SimpleMemberAccessExpression)
%178 = constant 1 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :55)
%179 = subi %177, %178 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :39)
%180 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :32) // usrnam[usrnam.Length - 1] (ElementAccessExpression)
%181 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :92 :26) // Binary expression on unsupported types "#" + usrnam[usrnam.Length - 1]
%183 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :93 :20) // Not a variable of known type: usr
%184 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :93 :20) // usr.Length (SimpleMemberAccessExpression)
%185 = constant 17 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :93 :34)
%186 = cmpi "sge", %184, %185 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :93 :20)
cond_br %186, ^6, ^7 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :93 :20)

^6: // SimpleBlock
%187 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :37) // Not a variable of known type: usr
%188 = constant 14 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :46)
%189 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :37) // usr.Take(14) (InvocationExpression)
%190 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :37) // usr.Take(14).ToArray() (InvocationExpression)
%191 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :26) // new string(usr.Take(14).ToArray()) (ObjectCreationExpression)
%192 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :63) // "..." (StringLiteralExpression)
%193 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :94 :26) // Binary expression on unsupported types new string(usr.Take(14).ToArray()) + "..."
br ^7

^7: // JumpBlock
%194 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :16) // Not a variable of known type: canv
%195 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :32) // Not a variable of known type: usr
%196 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :38) // Not a variable of known type: tag
%197 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :32) // Binary expression on unsupported types usr + tag
%198 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :52) // "Bahnschrift" (StringLiteralExpression)
%199 = constant 42 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :67)
%200 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :43) // new Font("Bahnschrift", 42) (ObjectCreationExpression)
// Entity from another assembly: System
%201 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :87) // System.Drawing.Color (SimpleMemberAccessExpression)
%202 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :87) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%203 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :72) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%204 = constant 260 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :127)
%205 = constant 32 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :132)
%206 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :116) // new PointF(260, 32) (ObjectCreationExpression)
%207 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :95 :16) // canv.DrawString(usr + tag, new Font("Bahnschrift", 42), new SolidBrush(System.Drawing.Color.White), new PointF(260, 32)) (InvocationExpression)
%208 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :16) // Not a variable of known type: canv
%209 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :32) // "Rank #" (StringLiteralExpression)
%210 = cbde.load %6 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :43)
%211 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :32) // Binary expression on unsupported types "Rank #" + Rank
%212 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :58) // "Bahnschrift" (StringLiteralExpression)
%213 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :73)
%214 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :49) // new Font("Bahnschrift", 30) (ObjectCreationExpression)
// Entity from another assembly: System
%215 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :93) // System.Drawing.Color (SimpleMemberAccessExpression)
%216 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :93) // System.Drawing.Color.Gold (SimpleMemberAccessExpression)
%217 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :78) // new SolidBrush(System.Drawing.Color.Gold) (ObjectCreationExpression)
%218 = constant 260 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :132)
%219 = constant 100 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :137)
%220 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :121) // new PointF(260, 100) (ObjectCreationExpression)
%221 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :97 :16) // canv.DrawString("Rank #" + Rank, new Font("Bahnschrift", 30), new SolidBrush(System.Drawing.Color.Gold), new PointF(260, 100)) (InvocationExpression)
%222 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :16) // Not a variable of known type: canv
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: KiloFormat
%223 = cbde.load %3 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :51)
%224 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :40) // KiloFormat(curXP) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: KiloFormat
%225 = cbde.load %4 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :73)
%226 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :62) // KiloFormat(nxtXP) (InvocationExpression)
%227 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :32) // $"XP:  {KiloFormat(curXP)} / {KiloFormat(nxtXP)}" (InterpolatedStringExpression)
%228 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :92) // "Bahnschrift" (StringLiteralExpression)
%229 = constant 20 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :107)
%230 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :83) // new Font("Bahnschrift", 20) (ObjectCreationExpression)
// Entity from another assembly: System
%231 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :127) // System.Drawing.Color (SimpleMemberAccessExpression)
%232 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :127) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%233 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :112) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%234 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :167) // Not a variable of known type: btmp
%235 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :167) // btmp.Width (SimpleMemberAccessExpression)
%236 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :180)
%237 = subi %235, %236 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :167)
%238 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :184) // Not a variable of known type: btmp
%239 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :184) // btmp.Height (SimpleMemberAccessExpression)
%240 = constant 100 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :198)
%241 = subi %239, %240 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :184)
%242 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :156) // new PointF(btmp.Width - 60, btmp.Height - 100) (ObjectCreationExpression)
// Entity from another assembly: StringFormatFlags
%243 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :221) // StringFormatFlags.DirectionRightToLeft (SimpleMemberAccessExpression)
%244 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :204) // new StringFormat(StringFormatFlags.DirectionRightToLeft) (ObjectCreationExpression)
%245 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :99 :16) // canv.DrawString($"XP:  {KiloFormat(curXP)} / {KiloFormat(nxtXP)}", new Font("Bahnschrift", 20), new SolidBrush(System.Drawing.Color.White), new PointF(btmp.Width - 60, btmp.Height - 100), new StringFormat(StringFormatFlags.DirectionRightToLeft)) (InvocationExpression)
%246 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :16) // Not a variable of known type: canv
%247 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :32) // "Level " (StringLiteralExpression)
%248 = cbde.load %2 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :43)
%249 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :32) // Binary expression on unsupported types "Level " + level
%250 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :59) // "Bahnschrift" (StringLiteralExpression)
%251 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :74)
%252 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :50) // new Font("Bahnschrift", 30) (ObjectCreationExpression)
// Entity from another assembly: System
%253 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :94) // System.Drawing.Color (SimpleMemberAccessExpression)
%254 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :94) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%255 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :79) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%256 = constant 260 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :134)
%257 = constant 150 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :139)
%258 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :123) // new PointF(260, 150) (ObjectCreationExpression)
%259 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :101 :16) // canv.DrawString("Level " + level, new Font("Bahnschrift", 30), new SolidBrush(System.Drawing.Color.White), new PointF(260, 150)) (InvocationExpression)
%260 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :102 :16) // Not a variable of known type: canv
%261 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :102 :16) // canv.Save() (InvocationExpression)
%262 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :103 :23) // Not a variable of known type: btmp
return %262 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :103 :16)

^8: // ExitBlock
cbde.unreachable

}
func @_Public_Bot.Modules.Commands.LevelCommands.RankBuilder.ResizeImage$System.Drawing.Image.int.int$(none, i32, i32) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :12) {
^entry (%_image : none, %_width : i32, %_height : i32):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :45)
cbde.store %_image, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :45)
%1 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :73)
cbde.store %_width, %1 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :73)
%2 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :84)
cbde.store %_height, %2 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :105 :84)
br ^0

^0: // JumpBlock
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :107 :34) // Not a variable of known type: image
%4 = cbde.load %1 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :107 :50)
%5 = cbde.load %2 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :107 :57)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :107 :41) // new Size(width, height) (ObjectCreationExpression)
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :107 :23) // new Bitmap(image, new Size(width, height)) (ObjectCreationExpression)
return %7 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :107 :16)

^1: // ExitBlock
cbde.unreachable

}
func @_Public_Bot.Modules.Commands.LevelCommands.RankBuilder.RoundedRect$System.Drawing.Rectangle.int$(none, i32) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :130 :12) {
^entry (%_bounds : none, %_radius : i32):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :130 :51)
cbde.store %_bounds, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :130 :51)
%1 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :130 :69)
cbde.store %_radius, %1 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :130 :69)
br ^0

^0: // BinaryBranchBlock
%2 = cbde.load %1 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :132 :31)
%3 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :132 :40)
%4 = muli %2, %3 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :132 :31)
%5 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :132 :20) // diameter
cbde.store %4, %5 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :132 :20)
%6 = cbde.load %5 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :133 :37)
%7 = cbde.load %5 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :133 :47)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :133 :28) // new Size(diameter, diameter) (ObjectCreationExpression)
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :134 :46) // Not a variable of known type: bounds
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :134 :46) // bounds.Location (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :134 :63) // Not a variable of known type: size
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :134 :32) // new Rectangle(bounds.Location, size) (ObjectCreationExpression)
%15 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :135 :36) // new GraphicsPath() (ObjectCreationExpression)
%17 = cbde.load %1 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :137 :20)
%18 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :137 :30)
%19 = cmpi "eq", %17, %18 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :137 :20)
cond_br %19, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :137 :20)

^1: // JumpBlock
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :139 :20) // Not a variable of known type: path
%21 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :139 :38) // Not a variable of known type: bounds
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :139 :20) // path.AddRectangle(bounds) (InvocationExpression)
%23 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :140 :27) // Not a variable of known type: path
return %23 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :140 :20)

^2: // JumpBlock
%24 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :144 :16) // Not a variable of known type: path
%25 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :144 :28) // Not a variable of known type: arc
%26 = constant 180 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :144 :33)
%27 = constant 90 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :144 :38)
%28 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :144 :16) // path.AddArc(arc, 180, 90) (InvocationExpression)
%29 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :147 :16) // Not a variable of known type: arc
%30 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :147 :16) // arc.X (SimpleMemberAccessExpression)
%31 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :147 :24) // Not a variable of known type: bounds
%32 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :147 :24) // bounds.Right (SimpleMemberAccessExpression)
%33 = cbde.load %5 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :147 :39)
%34 = subi %32, %33 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :147 :24)
%35 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :148 :16) // Not a variable of known type: path
%36 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :148 :28) // Not a variable of known type: arc
%37 = constant 270 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :148 :33)
%38 = constant 90 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :148 :38)
%39 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :148 :16) // path.AddArc(arc, 270, 90) (InvocationExpression)
%40 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :151 :16) // Not a variable of known type: arc
%41 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :151 :16) // arc.Y (SimpleMemberAccessExpression)
%42 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :151 :24) // Not a variable of known type: bounds
%43 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :151 :24) // bounds.Bottom (SimpleMemberAccessExpression)
%44 = cbde.load %5 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :151 :40)
%45 = subi %43, %44 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :151 :24)
%46 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :152 :16) // Not a variable of known type: path
%47 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :152 :28) // Not a variable of known type: arc
%48 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :152 :33)
%49 = constant 90 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :152 :36)
%50 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :152 :16) // path.AddArc(arc, 0, 90) (InvocationExpression)
%51 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :155 :16) // Not a variable of known type: arc
%52 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :155 :16) // arc.X (SimpleMemberAccessExpression)
%53 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :155 :24) // Not a variable of known type: bounds
%54 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :155 :24) // bounds.Left (SimpleMemberAccessExpression)
%55 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :156 :16) // Not a variable of known type: path
%56 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :156 :28) // Not a variable of known type: arc
%57 = constant 90 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :156 :33)
%58 = constant 90 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :156 :37)
%59 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :156 :16) // path.AddArc(arc, 90, 90) (InvocationExpression)
%60 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :158 :16) // Not a variable of known type: path
%61 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :158 :16) // path.CloseFigure() (InvocationExpression)
%62 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :159 :23) // Not a variable of known type: path
return %62 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :159 :16)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function ClipToCircle(none, none, none, none), it contains poisonous unsupported syntaxes

func @_Public_Bot.Modules.Commands.LevelCommands.RankBuilder.KiloFormat$int$(i32) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :188 :12) {
^entry (%_num : i32):
%0 = cbde.alloca i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :188 :44)
cbde.store %_num, %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :188 :44)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :190 :20)
%2 = constant 100000000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :190 :27)
%3 = cmpi "sge", %1, %2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :190 :20)
cond_br %3, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :190 :20)

^1: // JumpBlock
%4 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :191 :28)
%5 = constant 1000000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :191 :34)
%6 = divis %4, %5 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :191 :28)
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :191 :52) // "#,0M" (StringLiteralExpression)
%8 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :191 :27) // (num / 1000000).ToString("#,0M") (InvocationExpression)
return %8 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :191 :20)

^2: // BinaryBranchBlock
%9 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :193 :20)
%10 = constant 10000000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :193 :27)
%11 = cmpi "sge", %9, %10 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :193 :20)
cond_br %11, ^3, ^4 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :193 :20)

^3: // JumpBlock
%12 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :28)
%13 = constant 1000000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :34)
%14 = divis %12, %13 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :28)
%15 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :52) // "0.#" (StringLiteralExpression)
%16 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :27) // (num / 1000000).ToString("0.#") (InvocationExpression)
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :61) // "M" (StringLiteralExpression)
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :27) // Binary expression on unsupported types (num / 1000000).ToString("0.#") + "M"
return %18 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :194 :20)

^4: // BinaryBranchBlock
%19 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :196 :20)
%20 = constant 100000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :196 :27)
%21 = cmpi "sge", %19, %20 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :196 :20)
cond_br %21, ^5, ^6 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :196 :20)

^5: // JumpBlock
%22 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :197 :28)
%23 = constant 1000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :197 :34)
%24 = divis %22, %23 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :197 :28)
%25 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :197 :49) // "#,0K" (StringLiteralExpression)
%26 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :197 :27) // (num / 1000).ToString("#,0K") (InvocationExpression)
return %26 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :197 :20)

^6: // BinaryBranchBlock
%27 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :199 :20)
%28 = constant 10000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :199 :27)
%29 = cmpi "sge", %27, %28 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :199 :20)
cond_br %29, ^7, ^8 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :199 :20)

^7: // JumpBlock
%30 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :28)
%31 = constant 1000 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :34)
%32 = divis %30, %31 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :28)
%33 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :49) // "0.#" (StringLiteralExpression)
%34 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :27) // (num / 1000).ToString("0.#") (InvocationExpression)
%35 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :58) // "K" (StringLiteralExpression)
%36 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :27) // Binary expression on unsupported types (num / 1000).ToString("0.#") + "K"
return %36 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :200 :20)

^8: // JumpBlock
%37 = cbde.load %0 : memref<i32> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :202 :23)
%38 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :202 :36) // "#,0" (StringLiteralExpression)
%39 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :202 :23) // num.ToString("#,0") (InvocationExpression)
return %39 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\LevelCommands.cs" :202 :16)

^9: // ExitBlock
cbde.unreachable

}
// Skipping function bk(none, none), it contains poisonous unsupported syntaxes

// Skipping function rank(none), it contains poisonous unsupported syntaxes

// Skipping function rc(none), it contains poisonous unsupported syntaxes

// Skipping function sl(none), it contains poisonous unsupported syntaxes

// Skipping function sxp(none), it contains poisonous unsupported syntaxes

// Skipping function gl(none), it contains poisonous unsupported syntaxes

// Skipping function gxp(none), it contains poisonous unsupported syntaxes

// Skipping function LevelSettings(none), it contains poisonous unsupported syntaxes

