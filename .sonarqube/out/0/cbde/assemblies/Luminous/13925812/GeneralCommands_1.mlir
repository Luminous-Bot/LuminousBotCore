// Skipping function Tw(), it contains poisonous unsupported syntaxes

// Skipping function AvatarShows(none), it contains poisonous unsupported syntaxes

// Skipping function HHE(none), it contains poisonous unsupported syntaxes

// Skipping function Yu(none), it contains poisonous unsupported syntaxes

// Skipping function El(none), it contains poisonous unsupported syntaxes

// Skipping function help(none), it contains poisonous unsupported syntaxes

// Skipping function NicknameUpdate(none), it contains poisonous unsupported syntaxes

// Skipping function invite(), it contains poisonous unsupported syntaxes

// Skipping function WhoIs(none), it contains poisonous unsupported syntaxes

// Skipping function ping(), it contains poisonous unsupported syntaxes

func @_Public_Bot.Modules.Commands.GeneralCommands.GuildStatBuilder.MakeServerCard$string.string.string.string.string.string.System.DateTime$(none, none, none, none, none, none, none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :12) {
^entry (%_servername : none, %_serverlogo : none, %_bannerurl : none, %_owner : none, %_users : none, %_nitroboosts : none, %_Created : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :62)
cbde.store %_servername, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :62)
%1 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :81)
cbde.store %_serverlogo, %1 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :81)
%2 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :100)
cbde.store %_bannerurl, %2 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :100)
%3 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :118)
cbde.store %_owner, %3 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :118)
%4 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :132)
cbde.store %_users, %4 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :132)
%5 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :146)
cbde.store %_nitroboosts, %5 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :146)
%6 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :166)
cbde.store %_Created, %6 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :558 :166)
br ^0

^0: // BinaryBranchBlock
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :560 :31) // new WebClient() (ObjectCreationExpression)
%9 = constant 960 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :562 :41)
%10 = constant 540 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :562 :46)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :562 :30) // new Bitmap(960, 540) (ObjectCreationExpression)
// Entity from another assembly: Graphics
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :563 :43) // Not a variable of known type: baseImg
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :563 :24) // Graphics.FromImage(baseImg) (InvocationExpression)
%16 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :564 :16) // Not a variable of known type: g
%17 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :564 :16) // g.SmoothingMode (SimpleMemberAccessExpression)
// Entity from another assembly: SmoothingMode
%18 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :564 :34) // SmoothingMode.AntiAlias (SimpleMemberAccessExpression)
%19 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :566 :20) // Not a variable of known type: bannerurl
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :566 :33) // null (NullLiteralExpression)
%21 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :566 :20) // comparison of unknown type: bannerurl == null
cond_br %21, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :566 :20)

^1: // SimpleBlock
%22 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :20) // Not a variable of known type: g
// Entity from another assembly: System
%23 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :46) // System.Drawing.Color (SimpleMemberAccessExpression)
%24 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :76)
%25 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :80)
%26 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :84)
%27 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :46) // System.Drawing.Color.FromArgb(40, 40, 40) (InvocationExpression)
%28 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :31) // new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)) (ObjectCreationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%29 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :90) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%30 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :142)
%31 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :145)
%32 = constant 960 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :148)
%33 = constant 540 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :153)
%34 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :128) // new Rectangle(0, 0, 960, 540) (ObjectCreationExpression)
%35 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :159)
%36 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :90) // LevelCommands.RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30) (InvocationExpression)
%37 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :567 :20) // g.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30)) (InvocationExpression)
br ^3

^2: // SimpleBlock
%38 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :570 :35) // Not a variable of known type: wc
%39 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :570 :51) // Not a variable of known type: bannerurl
%40 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :570 :35) // wc.DownloadData(bannerurl) (InvocationExpression)
%42 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :571 :55) // Not a variable of known type: bytes
%43 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :571 :38) // new MemoryStream(bytes) (ObjectCreationExpression)
// Entity from another assembly: System
%45 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :572 :49) // System.Drawing.Image (SimpleMemberAccessExpression)
%46 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :572 :81) // Not a variable of known type: ms
%47 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :572 :49) // System.Drawing.Image.FromStream(ms) (InvocationExpression)
%49 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :20) // Not a variable of known type: g
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: RoundCorners
%50 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :45) // Not a variable of known type: bannr
%51 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :52)
%52 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :32) // RoundCorners(bannr, 30) (InvocationExpression)
%53 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :68)
%54 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :71)
%55 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :57) // new PointF(0, 0) (ObjectCreationExpression)
%56 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :573 :20) // g.DrawImage(RoundCorners(bannr, 30), new PointF(0, 0)) (InvocationExpression)
%57 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :20) // Not a variable of known type: g
// Entity from another assembly: System
%58 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :46) // System.Drawing.Color (SimpleMemberAccessExpression)
%59 = constant 200 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :76)
%60 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :81)
%61 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :85)
%62 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :89)
%63 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :46) // System.Drawing.Color.FromArgb(200, 40, 40, 40) (InvocationExpression)
%64 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :31) // new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)) (ObjectCreationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%65 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :95) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%66 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :147)
%67 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :151)
%68 = constant 900 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :155)
%69 = constant 480 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :160)
%70 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :133) // new Rectangle(30, 30, 900, 480) (ObjectCreationExpression)
%71 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :166)
%72 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :95) // LevelCommands.RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30) (InvocationExpression)
%73 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :574 :20) // g.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30)) (InvocationExpression)
br ^3

^3: // BinaryBranchBlock
%74 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :577 :19) // Not a variable of known type: serverlogo
%75 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :577 :33) // null (NullLiteralExpression)
%76 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :577 :19) // comparison of unknown type: serverlogo != null
cond_br %76, ^4, ^5 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :577 :19)

^4: // SimpleBlock
%77 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :579 :36) // Not a variable of known type: wc
%78 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :579 :52) // Not a variable of known type: serverlogo
%79 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :579 :36) // wc.DownloadData(serverlogo) (InvocationExpression)
%81 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :580 :56) // Not a variable of known type: bytes2
%82 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :580 :39) // new MemoryStream(bytes2) (ObjectCreationExpression)
// Entity from another assembly: System
%84 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :581 :30) // System.Drawing.Image (SimpleMemberAccessExpression)
%85 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :581 :62) // Not a variable of known type: ms2
%86 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :581 :30) // System.Drawing.Image.FromStream(ms2) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%88 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :582 :31) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%89 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :582 :69) // Not a variable of known type: img
%90 = constant 150 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :582 :74)
%91 = constant 150 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :582 :79)
%92 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :582 :31) // LevelCommands.RankBuilder.ResizeImage(img, 150, 150) (InvocationExpression)
%94 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :583 :20) // Not a variable of known type: img
%95 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :583 :20) // img.Dispose() (InvocationExpression)
%96 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :20) // Not a variable of known type: g
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%97 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :32) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%98 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :71) // Not a variable of known type: Icon
%99 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :88) // Not a variable of known type: Icon
%100 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :88) // Icon.Width (SimpleMemberAccessExpression)
%101 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :101)
%102 = divis %100, %101 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :88)
%103 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :104) // Not a variable of known type: Icon
%104 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :104) // Icon.Height (SimpleMemberAccessExpression)
%105 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :118)
%106 = divis %104, %105 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :104)
%107 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :77) // new PointF(Icon.Width / 2, Icon.Height / 2) (ObjectCreationExpression)
%108 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :122) // Not a variable of known type: Icon
%109 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :122) // Icon.Width (SimpleMemberAccessExpression)
%110 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :135)
%111 = divis %109, %110 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :122)
// Entity from another assembly: System
%112 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :138) // System.Drawing.Color (SimpleMemberAccessExpression)
%113 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :138) // System.Drawing.Color.Transparent (SimpleMemberAccessExpression)
%114 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :32) // LevelCommands.RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent) (InvocationExpression)
%115 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :184) // Not a variable of known type: baseImg
%116 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :184) // baseImg.Width (SimpleMemberAccessExpression)
%117 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :200)
%118 = divis %116, %117 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :184)
%119 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :204) // Not a variable of known type: Icon
%120 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :204) // Icon.Width (SimpleMemberAccessExpression)
%121 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :217)
%122 = divis %120, %121 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :204)
%123 = subi %118, %122 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :184)
%124 = constant 120 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :220)
%125 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :173) // new PointF(baseImg.Width / 2 - Icon.Width / 2, 120) (ObjectCreationExpression)
%126 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :585 :20) // g.DrawImage(LevelCommands.RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent), new PointF(baseImg.Width / 2 - Icon.Width / 2, 120)) (InvocationExpression)
br ^5

^5: // JumpBlock
%127 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :587 :44) // new StringFormat() (ObjectCreationExpression)
%129 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :588 :16) // Not a variable of known type: stringFormat
%130 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :588 :16) // stringFormat.Alignment (SimpleMemberAccessExpression)
// Entity from another assembly: StringAlignment
%131 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :588 :41) // StringAlignment.Center (SimpleMemberAccessExpression)
%132 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :589 :16) // Not a variable of known type: stringFormat
%133 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :589 :16) // stringFormat.LineAlignment (SimpleMemberAccessExpression)
// Entity from another assembly: StringAlignment
%134 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :589 :45) // StringAlignment.Center (SimpleMemberAccessExpression)
%135 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :55)
%136 = constant 180 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :59)
%137 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :64) // Not a variable of known type: baseImg
%138 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :64) // baseImg.Width (SimpleMemberAccessExpression)
%139 = constant 80 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :80)
%140 = subi %138, %139 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :64)
%141 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :84) // Not a variable of known type: baseImg
%142 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :84) // baseImg.Height (SimpleMemberAccessExpression)
%143 = constant 100 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :101)
%144 = subi %142, %143 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :84)
%145 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :591 :40) // new RectangleF(40, 180, baseImg.Width - 80, baseImg.Height - 100) (ObjectCreationExpression)
%147 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :592 :36) // "Bahnschrift" (StringLiteralExpression)
%148 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :592 :51)
// Entity from another assembly: FontStyle
%149 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :592 :55) // FontStyle.Regular (SimpleMemberAccessExpression)
%150 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :592 :27) // new Font("Bahnschrift", 30, FontStyle.Regular) (ObjectCreationExpression)
// Entity from another assembly: System
%152 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :593 :43) // System.Drawing.Color (SimpleMemberAccessExpression)
%153 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :593 :43) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%154 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :593 :28) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%156 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :16) // Not a variable of known type: g
%157 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :29) // Not a variable of known type: servername
%158 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :50) // "Bahnschrift" (StringLiteralExpression)
%159 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :65)
%160 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :41) // new Font("Bahnschrift", 40) (ObjectCreationExpression)
// Entity from another assembly: System
%161 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :85) // System.Drawing.Color (SimpleMemberAccessExpression)
%162 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :85) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%163 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :70) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%164 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :129)
%165 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :133)
%166 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :137) // Not a variable of known type: baseImg
%167 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :137) // baseImg.Width (SimpleMemberAccessExpression)
%168 = constant 120 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :153)
%169 = subi %167, %168 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :137)
%170 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :158)
%171 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :114) // new RectangleF(60, 40, baseImg.Width - 120, 60) (ObjectCreationExpression)
%172 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :163) // Not a variable of known type: stringFormat
%173 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :594 :16) // g.DrawString(servername, new Font("Bahnschrift", 40), new SolidBrush(System.Drawing.Color.White), new RectangleF(60, 40, baseImg.Width - 120, 60), stringFormat) (InvocationExpression)
%174 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :16) // Not a variable of known type: g
%175 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :39) // Not a variable of known type: owner
%176 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :29) // $"Owner: {owner}" (InterpolatedStringExpression)
%177 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :48) // Not a variable of known type: font
%178 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :54) // Not a variable of known type: brush
%179 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :72) // Not a variable of known type: baseImg
%180 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :72) // baseImg.Width (SimpleMemberAccessExpression)
%181 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :88)
%182 = divis %180, %181 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :72)
%183 = constant 310 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :91)
%184 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :61) // new PointF(baseImg.Width / 2, 310) (ObjectCreationExpression)
%185 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :97) // Not a variable of known type: stringFormat
%186 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :596 :16) // g.DrawString($"Owner: {owner}", font, brush, new PointF(baseImg.Width / 2, 310), stringFormat) (InvocationExpression)
%187 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :16) // Not a variable of known type: g
%188 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :39) // Not a variable of known type: users
%189 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :29) // $"Users: {users}" (InterpolatedStringExpression)
%190 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :48) // Not a variable of known type: font
%191 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :54) // Not a variable of known type: brush
%192 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :72) // Not a variable of known type: baseImg
%193 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :72) // baseImg.Width (SimpleMemberAccessExpression)
%194 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :88)
%195 = divis %193, %194 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :72)
%196 = constant 360 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :91)
%197 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :61) // new PointF(baseImg.Width / 2, 360) (ObjectCreationExpression)
%198 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :97) // Not a variable of known type: stringFormat
%199 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :597 :16) // g.DrawString($"Users: {users}", font, brush, new PointF(baseImg.Width / 2, 360), stringFormat) (InvocationExpression)
%200 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :16) // Not a variable of known type: g
%201 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :48) // Not a variable of known type: nitroboosts
%202 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :29) // $"Nitro Boosters: {nitroboosts}" (InterpolatedStringExpression)
%203 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :63) // Not a variable of known type: font
%204 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :69) // Not a variable of known type: brush
%205 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :87) // Not a variable of known type: baseImg
%206 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :87) // baseImg.Width (SimpleMemberAccessExpression)
%207 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :103)
%208 = divis %206, %207 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :87)
%209 = constant 410 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :106)
%210 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :76) // new PointF(baseImg.Width / 2, 410) (ObjectCreationExpression)
%211 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :112) // Not a variable of known type: stringFormat
%212 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :598 :16) // g.DrawString($"Nitro Boosters: {nitroboosts}", font, brush, new PointF(baseImg.Width / 2, 410), stringFormat) (InvocationExpression)
%213 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :16) // Not a variable of known type: g
%214 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :44) // Not a variable of known type: Created
%215 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :61) // "r" (StringLiteralExpression)
%216 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :44) // Created.ToString("r") (InvocationExpression)
%217 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :29) // $"Created on: {Created.ToString("r")}" (InterpolatedStringExpression)
%218 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :69) // Not a variable of known type: font
%219 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :75) // Not a variable of known type: brush
%220 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :93) // Not a variable of known type: baseImg
%221 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :93) // baseImg.Width (SimpleMemberAccessExpression)
%222 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :109)
%223 = divis %221, %222 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :93)
%224 = constant 460 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :112)
%225 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :82) // new PointF(baseImg.Width / 2, 460) (ObjectCreationExpression)
%226 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :118) // Not a variable of known type: stringFormat
%227 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :599 :16) // g.DrawString($"Created on: {Created.ToString("r")}", font, brush, new PointF(baseImg.Width / 2, 460), stringFormat) (InvocationExpression)
%228 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :604 :16) // Not a variable of known type: g
%229 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :604 :16) // g.Save() (InvocationExpression)
%230 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :605 :23) // Not a variable of known type: baseImg
return %230 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Commands\\GeneralCommands.cs" :605 :16)

^6: // ExitBlock
cbde.unreachable

}
// Skipping function RoundCorners(none, i32, i32, i32), it contains poisonous unsupported syntaxes

// Skipping function stats(), it contains poisonous unsupported syntaxes

