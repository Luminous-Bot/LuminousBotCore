// Skipping function Client_UserJoined(none), it contains poisonous unsupported syntaxes

func @_Public_Bot.Modules.Handlers.WelcomeHandler.GenerateWelcomeImage$Discord.WebSocket.SocketGuildUser.Discord.WebSocket.SocketGuild.Public_Bot.WelcomeCard$(none, none, none) -> none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :8) {
^entry (%_user : none, %_guild : none, %_welc : none):
%0 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :49)
cbde.store %_user, %0 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :49)
%1 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :71)
cbde.store %_guild, %1 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :71)
%2 = cbde.alloca none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :90)
cbde.store %_welc, %2 : memref<none> loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :56 :90)
br ^0

^0: // BinaryBranchBlock
%3 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :58 :12) // Not a variable of known type: WelcomeGraphics
// Entity from another assembly: Color
%4 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :58 :34) // Color.Transparent (SimpleMemberAccessExpression)
%5 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :58 :12) // WelcomeGraphics.Clear(Color.Transparent) (InvocationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :59 :12) // Not a variable of known type: WelcomeGraphics
%7 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :59 :12) // WelcomeGraphics.SmoothingMode (SimpleMemberAccessExpression)
// Entity from another assembly: SmoothingMode
%8 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :59 :44) // SmoothingMode.AntiAlias (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :62 :16) // Not a variable of known type: welc
%10 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :62 :16) // welc.BackgroundUrl (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :62 :38) // null (NullLiteralExpression)
%12 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :62 :16) // comparison of unknown type: welc.BackgroundUrl == null
cond_br %12, ^1, ^2 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :62 :16)

^1: // SimpleBlock
%13 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :16) // Not a variable of known type: WelcomeGraphics
// Entity from another assembly: System
%14 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :56) // System.Drawing.Color (SimpleMemberAccessExpression)
%15 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :86)
%16 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :90)
%17 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :94)
%18 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :56) // System.Drawing.Color.FromArgb(40, 40, 40) (InvocationExpression)
%19 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :41) // new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)) (ObjectCreationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%20 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :100) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%21 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :152)
%22 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :155)
%23 = constant 960 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :158)
%24 = constant 540 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :163)
%25 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :138) // new Rectangle(0, 0, 960, 540) (ObjectCreationExpression)
%26 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :169)
%27 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :100) // LevelCommands.RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30) (InvocationExpression)
%28 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :63 :16) // WelcomeGraphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(0, 0, 960, 540), 30)) (InvocationExpression)
br ^3

^2: // SimpleBlock
%29 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :66 :31) // Not a variable of known type: wc
%30 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :66 :47) // Not a variable of known type: welc
%31 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :66 :47) // welc.BackgroundUrl (SimpleMemberAccessExpression)
%32 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :66 :31) // wc.DownloadData(welc.BackgroundUrl) (InvocationExpression)
%34 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :67 :51) // Not a variable of known type: bytes
%35 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :67 :34) // new MemoryStream(bytes) (ObjectCreationExpression)
// Entity from another assembly: System
%37 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :68 :45) // System.Drawing.Image (SimpleMemberAccessExpression)
%38 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :68 :77) // Not a variable of known type: ms
%39 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :68 :45) // System.Drawing.Image.FromStream(ms) (InvocationExpression)
%41 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :16) // Not a variable of known type: WelcomeGraphics
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GuildStatBuilder
%42 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :72) // Not a variable of known type: bannr
%43 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :79)
%44 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :42) // GuildStatBuilder.RoundCorners(bannr, 30) (InvocationExpression)
%45 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :95)
%46 = constant 0 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :98)
%47 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :84) // new PointF(0, 0) (ObjectCreationExpression)
%48 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :69 :16) // WelcomeGraphics.DrawImage(GuildStatBuilder.RoundCorners(bannr, 30), new PointF(0, 0)) (InvocationExpression)
%49 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :16) // Not a variable of known type: WelcomeGraphics
// Entity from another assembly: System
%50 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :56) // System.Drawing.Color (SimpleMemberAccessExpression)
%51 = constant 200 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :86)
%52 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :91)
%53 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :95)
%54 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :99)
%55 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :56) // System.Drawing.Color.FromArgb(200, 40, 40, 40) (InvocationExpression)
%56 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :41) // new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)) (ObjectCreationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%57 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :105) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%58 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :157)
%59 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :161)
%60 = constant 900 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :165)
%61 = constant 480 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :170)
%62 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :143) // new Rectangle(30, 30, 900, 480) (ObjectCreationExpression)
%63 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :176)
%64 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :105) // LevelCommands.RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30) (InvocationExpression)
%65 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :70 :16) // WelcomeGraphics.FillPath(new SolidBrush(System.Drawing.Color.FromArgb(200, 40, 40, 40)), LevelCommands.RankBuilder.RoundedRect(new Rectangle(30, 30, 900, 480), 30)) (InvocationExpression)
br ^3

^3: // BinaryBranchBlock
%66 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :74 :16) // Not a variable of known type: guild
%67 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :74 :16) // guild.IconUrl (SimpleMemberAccessExpression)
%68 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :74 :33) // null (NullLiteralExpression)
%69 = cbde.unknown : i1  loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :74 :16) // comparison of unknown type: guild.IconUrl != null
cond_br %69, ^4, ^5 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :74 :16)

^4: // SimpleBlock
%70 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :76 :32) // Not a variable of known type: wc
%71 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :76 :48) // Not a variable of known type: guild
%72 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :76 :48) // guild.IconUrl (SimpleMemberAccessExpression)
%73 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :76 :32) // wc.DownloadData(guild.IconUrl) (InvocationExpression)
%75 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :77 :52) // Not a variable of known type: bytes2
%76 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :77 :35) // new MemoryStream(bytes2) (ObjectCreationExpression)
// Entity from another assembly: System
%78 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :78 :26) // System.Drawing.Image (SimpleMemberAccessExpression)
%79 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :78 :58) // Not a variable of known type: ms2
%80 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :78 :26) // System.Drawing.Image.FromStream(ms2) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%82 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :79 :27) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%83 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :79 :65) // Not a variable of known type: img
%84 = constant 150 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :79 :70)
%85 = constant 150 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :79 :75)
%86 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :79 :27) // LevelCommands.RankBuilder.ResizeImage(img, 150, 150) (InvocationExpression)
%88 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :80 :16) // Not a variable of known type: img
%89 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :80 :16) // img.Dispose() (InvocationExpression)
%90 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :16) // Not a variable of known type: WelcomeGraphics
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LevelCommands
%91 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :42) // LevelCommands.RankBuilder (SimpleMemberAccessExpression)
%92 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :81) // Not a variable of known type: Icon
%93 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :98) // Not a variable of known type: Icon
%94 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :98) // Icon.Width (SimpleMemberAccessExpression)
%95 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :111)
%96 = divis %94, %95 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :98)
%97 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :114) // Not a variable of known type: Icon
%98 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :114) // Icon.Height (SimpleMemberAccessExpression)
%99 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :128)
%100 = divis %98, %99 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :114)
%101 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :87) // new PointF(Icon.Width / 2, Icon.Height / 2) (ObjectCreationExpression)
%102 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :132) // Not a variable of known type: Icon
%103 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :132) // Icon.Width (SimpleMemberAccessExpression)
%104 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :145)
%105 = divis %103, %104 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :132)
// Entity from another assembly: System
%106 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :148) // System.Drawing.Color (SimpleMemberAccessExpression)
%107 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :148) // System.Drawing.Color.Transparent (SimpleMemberAccessExpression)
%108 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :42) // LevelCommands.RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent) (InvocationExpression)
%109 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :194) // Not a variable of known type: WelcomeImage
%110 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :194) // WelcomeImage.Width (SimpleMemberAccessExpression)
%111 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :215)
%112 = divis %110, %111 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :194)
%113 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :219) // Not a variable of known type: Icon
%114 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :219) // Icon.Width (SimpleMemberAccessExpression)
%115 = constant 2 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :232)
%116 = divis %114, %115 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :219)
%117 = subi %112, %116 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :194)
%118 = constant 120 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :235)
%119 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :183) // new PointF(WelcomeImage.Width / 2 - Icon.Width / 2, 120) (ObjectCreationExpression)
%120 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :81 :16) // WelcomeGraphics.DrawImage(LevelCommands.RankBuilder.ClipToCircle(Icon, new PointF(Icon.Width / 2, Icon.Height / 2), Icon.Width / 2, System.Drawing.Color.Transparent), new PointF(WelcomeImage.Width / 2 - Icon.Width / 2, 120)) (InvocationExpression)
br ^5

^5: // JumpBlock
%121 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :84 :40) // new StringFormat() (ObjectCreationExpression)
%123 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :85 :12) // Not a variable of known type: stringFormat
%124 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :85 :12) // stringFormat.Alignment (SimpleMemberAccessExpression)
// Entity from another assembly: StringAlignment
%125 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :85 :37) // StringAlignment.Center (SimpleMemberAccessExpression)
%126 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :86 :12) // Not a variable of known type: stringFormat
%127 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :86 :12) // stringFormat.FormatFlags (SimpleMemberAccessExpression)
// Entity from another assembly: StringFormatFlags
%128 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :86 :39) // StringFormatFlags.LineLimit (SimpleMemberAccessExpression)
%129 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :87 :12) // Not a variable of known type: stringFormat
%130 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :87 :12) // stringFormat.Trimming (SimpleMemberAccessExpression)
// Entity from another assembly: StringTrimming
%131 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :87 :36) // StringTrimming.None (SimpleMemberAccessExpression)
%132 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :88 :12) // Not a variable of known type: stringFormat
%133 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :88 :12) // stringFormat.LineAlignment (SimpleMemberAccessExpression)
// Entity from another assembly: StringAlignment
%134 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :88 :41) // StringAlignment.Center (SimpleMemberAccessExpression)
%135 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :90 :32) // "Bahnschrift" (StringLiteralExpression)
%136 = constant 30 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :90 :47)
// Entity from another assembly: FontStyle
%137 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :90 :51) // FontStyle.Regular (SimpleMemberAccessExpression)
%138 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :90 :23) // new Font("Bahnschrift", 30, FontStyle.Regular) (ObjectCreationExpression)
// Entity from another assembly: System
%140 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :91 :39) // System.Drawing.Color (SimpleMemberAccessExpression)
%141 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :91 :39) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%142 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :91 :24) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%144 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :12) // Not a variable of known type: WelcomeGraphics
%145 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :39) // Not a variable of known type: guild
%146 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :39) // guild.Name (SimpleMemberAccessExpression)
%147 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :60) // "Bahnschrift" (StringLiteralExpression)
%148 = constant 40 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :75)
// Entity from another assembly: FontStyle
%149 = constant unit loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :79) // FontStyle.Regular (SimpleMemberAccessExpression)
%150 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :51) // new Font("Bahnschrift", 40, FontStyle.Regular) (ObjectCreationExpression)
// Entity from another assembly: System
%151 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :114) // System.Drawing.Color (SimpleMemberAccessExpression)
%152 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :114) // System.Drawing.Color.White (SimpleMemberAccessExpression)
%153 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :99) // new SolidBrush(System.Drawing.Color.White) (ObjectCreationExpression)
%154 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :158)
%155 = constant 50 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :162)
%156 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :166) // Not a variable of known type: WelcomeImage
%157 = cbde.unknown : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :166) // WelcomeImage.Width (SimpleMemberAccessExpression)
%158 = constant 120 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :187)
%159 = subi %157, %158 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :166)
%160 = constant 60 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :192)
%161 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :143) // new RectangleF(60, 50, WelcomeImage.Width - 120, 60) (ObjectCreationExpression)
%162 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :197) // Not a variable of known type: stringFormat
%163 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :92 :12) // WelcomeGraphics.DrawString(guild.Name, new Font("Bahnschrift", 40, FontStyle.Regular), new SolidBrush(System.Drawing.Color.White), new RectangleF(60, 50, WelcomeImage.Width - 120, 60), stringFormat) (InvocationExpression)
%164 = constant 50 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :93 :41)
%165 = constant 310 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :93 :45)
%166 = constant 860 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :93 :50)
%167 = constant 200 : i32 loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :93 :55)
%168 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :93 :27) // new Rectangle(50, 310, 860, 200) (ObjectCreationExpression)
%170 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :12) // Not a variable of known type: WelcomeGraphics
%171 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :42) // Not a variable of known type: welc
%172 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :70) // Not a variable of known type: user
%173 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :76) // Not a variable of known type: guild
%174 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :42) // welc.GenerateWelcomeMessage(user, guild) (InvocationExpression)
%175 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :39) // $"{welc.GenerateWelcomeMessage(user, guild)}" (InterpolatedStringExpression)
%176 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :86) // Not a variable of known type: font
%177 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :92) // Not a variable of known type: brush
%178 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :99) // Not a variable of known type: textArea
%179 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :109) // Not a variable of known type: stringFormat
%180 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :95 :12) // WelcomeGraphics.DrawString($"{welc.GenerateWelcomeMessage(user, guild)}", font, brush, textArea, stringFormat) (InvocationExpression)
%181 = cbde.unknown : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :97 :19) // Not a variable of known type: WelcomeImage
return %181 : none loc("C:\\Users\\plynch\\source\\repos\\Public Bot\\Public Bot\\Modules\\Handlers\\WelcomeHandler.cs" :97 :12)

^6: // ExitBlock
cbde.unreachable

}
