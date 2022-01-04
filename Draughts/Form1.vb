Public Class Form1
    Public Structure position
        Dim pos As Point
        Dim colour As Integer
        Dim Size As Integer '1 = normal 2 = king
    End Structure
    Public Structure Move
        Dim Origin As position
        Dim Target As position
        Dim istake As Boolean
        Dim takepos As Point
        Dim TakeSize As Integer
        Dim Promotion As Boolean
        Dim score As Integer
    End Structure
    Public DisplayedMoves As List(Of Move)
    Public Board(7, 7) As Piece
    Public ImagesStore(5) As Image '0 blank light, 1 blank dark
    Public assetsPATH As String = "D:\documents\Visual Studio 2010\Projects\Draughts\Assets\"
    Public WhosTurn As Integer = 1 ' 1 for white, -1 black
    Public PlayingAi As Boolean = False
    Public AllMoves As List(Of Move)
    Public AlreadySelected As Boolean = False
    Public SelectedPiece As Point
    Public WhiteCount As Integer
    Public BlackCount As Integer
    Public BlackAI As AiPlayer
    Public WhiteAI As AiPlayer
    Public BestMoveScore As Integer
    Public NegaMaxCount As Integer = 0
    Public IsAICalculating As Boolean
    Public AiVsAi As Boolean
    Public ListOfWhitePieces As List(Of Point)
    Public ListOfblackPieces As List(Of Point)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllMoves = New List(Of Move)
        DisplayedMoves = New List(Of Move)
        AiButt.BackColor = Color.Red
        ListOfblackPieces = New List(Of Point)
        ListOfWhitePieces = New List(Of Point)
        BlackAI = New AiPlayer(-1)
        WhiteAI = New AiPlayer(1)

        ImagesStore(0) = Image.FromFile(assetsPATH & "BlankLightSquare.png")
        ImagesStore(1) = Image.FromFile(assetsPATH & "BlankDarkSquare.png")

        ImagesStore(2) = Image.FromFile(assetsPATH & "WhiteLightSquare.png")
        ImagesStore(3) = Image.FromFile(assetsPATH & "BlackLightSquare.png")

        ImagesStore(4) = Image.FromFile(assetsPATH & "WhiteDarkSquare.png")
        ImagesStore(5) = Image.FromFile(assetsPATH & "BlackDarkSquare.png")

        CreateOGBoard()
        Session()
    End Sub
    Private Sub CreateOGBoard()
        Dim SIDTemp As Integer = 1
        Dim TempColour As Integer
        Dim count As Integer = 0
        For y = 0 To 7
            SIDTemp = -SIDTemp
            For x = 0 To 7
                count += 1
                SIDTemp = -SIDTemp

                Board(x, y) = New Piece(x, y, SIDTemp, GetSetUpColour(x, y, count))

            Next
        Next
    End Sub
    Public Sub PieceCount()
        WhiteCount = 0
        BlackCount = 0
        For x = 0 To 7
            For y = 0 To 7
                Select Case Board(x, y).GetColour
                    Case 1
                        WhiteCount += 1
                    Case -1
                        BlackCount += 1
                End Select
            Next
        Next
    End Sub

    Public Sub DrawDisplayedMoves(ByVal moves As List(Of Move))
        For Each SMove In moves
            Board(SMove.Target.pos.X, SMove.Target.pos.Y).SetDisplayedSingleMove()
        Next
    End Sub
    Public Sub ClearDisplayedMoves()
        For Each SMove In DisplayedMoves
            Board(SMove.Target.pos.X, SMove.Target.pos.Y).SetImage()
        Next
    End Sub
    Private Function GetSetUpColour(ByVal x As Integer, ByVal y As Integer, ByVal count As Integer)

        Select Case y
            Case 0
                If x Mod 2 = 1 Then
                    Return -1
                End If
            Case 1
                If x Mod 2 = 0 Then
                    Return -1
                End If
            Case 2
                If x Mod 2 = 1 Then
                    Return -1
                End If


            Case 5
                If x Mod 2 = 0 Then
                    Return 1
                End If
            Case 6
                If x Mod 2 = 1 Then

                    Return 1
                End If
            Case 7
                If x Mod 2 = 0 Then
                    Return 1
                End If


            Case Else
                Return 0
        End Select
    End Function
    Private Sub DisplayBoard(ByVal board(,) As Piece)
        For X = 0 To 7
            For Y = 0 To 7
                board(X, Y).DrawPiece()
            Next
        Next
    End Sub

    Public Sub Session()
        Dim tempMove As Move
        If PlayingAi = True Or AiVsAi = True Then
            If AiVsAi = True Then
                Threading.Thread.Sleep(100)
                Application.DoEvents()
            End If
            WhosTurn = -WhosTurn
            IsAICalculating = True
            NegaMaxCount = 0


            tempMove = BlackAI.StartNegaMax(Board, WhosTurn, 4, WhosTurn)
            IsAICalculating = False
            MakeMove(tempMove)



        End If

        WhosTurn = -WhosTurn
        If AiVsAi = True Then
            Threading.Thread.Sleep(100)
            Application.DoEvents()
            IsAICalculating = True
            NegaMaxCount = 0


            tempMove = WhiteAI.StartNegaMax(Board, WhosTurn, 4, WhosTurn)
            IsAICalculating = False
            MakeMove(tempMove)

            Session()
        Else
            GenerateAllMoves(WhosTurn)
        End If





    End Sub
    Public Sub GenerateAllMoves(ByVal whosGo As Integer)
        AllMoves.Clear()
        For x = 0 To 7
            For y = 0 To 7
                If Board(x, y).GetColour = whosGo Then
                    Board(x, y).GenerateMoves()
                End If
            Next
        Next
    End Sub
    Public Function GenerateAllMovesAI(ByVal whosGo As Integer, ByVal allMove As List(Of Move), ByVal depth As Integer, ByVal takes As Boolean)
        allMove.Clear()
        Dim tempPoint As Point
        ListOfWhitePieces.Clear()
        ListOfWhitePieces.Clear()
        For x = 0 To 7
            For y = 0 To 7
                Board(x, y).ClearMoves()
                If Board(x, y).GetColour = whosGo Then
                    tempPoint.X = x
                    tempPoint.Y = y


                    Board(x, y).GenerateMoves()
                    For Each M In Board(x, y).GetMoves
                        If takes = True Then
                            If M.istake = True Then
                                allMove.Add(M)

                            End If
                        Else
                            allMove.Add(M)
                        End If

                    Next
                End If
                If Board(x, y).GetColour <> 0 And depth = 1 Then
                    If whosGo = -1 Then
                        ListOfblackPieces.Add(tempPoint)
                    Else
                        ListOfWhitePieces.Add(tempPoint)
                    End If
                End If
            Next
        Next
        Return allMove
    End Function
    Public Sub MakeMove(ByRef Move As Move)
        Select Case Move.Origin.colour
            Case -1
                If Move.Target.pos.Y = 7 Then
                    Board(Move.Origin.pos.X, Move.Origin.pos.Y).UpdateSquare(0, 0)
                    Board(Move.Target.pos.X, Move.Target.pos.Y).UpdateSquare(Move.Origin.colour, 2)
                    Move.Promotion = True
                Else
                    Board(Move.Origin.pos.X, Move.Origin.pos.Y).UpdateSquare(0, 0)
                    Board(Move.Target.pos.X, Move.Target.pos.Y).UpdateSquare(Move.Origin.colour, Move.Origin.Size)
                    Move.Promotion = False
                End If
            Case 1
                If Move.Target.pos.Y = 0 Then
                    Board(Move.Origin.pos.X, Move.Origin.pos.Y).UpdateSquare(0, 0)
                    Board(Move.Target.pos.X, Move.Target.pos.Y).UpdateSquare(Move.Origin.colour, 2)
                    Move.Promotion = True
                Else
                    Board(Move.Origin.pos.X, Move.Origin.pos.Y).UpdateSquare(0, 0)
                    Board(Move.Target.pos.X, Move.Target.pos.Y).UpdateSquare(Move.Origin.colour, Move.Origin.Size)
                    Move.Promotion = False
                End If
        End Select


        If Move.istake = True Then
            Board(Move.takepos.X, Move.takepos.Y).UpdateSquare(0, 0)

        End If
    End Sub

    Public Sub UnmakeMove(ByVal move As Move)
        Board(move.Target.pos.X, move.Target.pos.Y).UpdateSquare(0, 0)
        Board(move.Origin.pos.X, move.Origin.pos.Y).UpdateSquare(move.Origin.colour, move.Origin.Size)
        If move.istake = True Then
            Dim tempColour As Integer
            If move.Origin.colour = 1 Then
                tempColour = -1
            Else
                tempColour = 1
            End If

            Board(move.takepos.X, move.takepos.Y).UpdateSquare(tempColour, move.TakeSize)
        End If
        
    End Sub

    Private Sub AiButt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AiButt.Click
        If PlayingAi = False Then
            If AiVsAi = True Then
                AiVsAi = False
                AIVSAIBUtt.BackColor = Color.Red
            End If
            PlayingAi = True
            AiButt.BackColor = Color.ForestGreen
        Else
            PlayingAi = False
            AiButt.BackColor = Color.Red
        End If
    End Sub

    Private Sub AIVSAIBUtt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AIVSAIBUtt.Click
        If AiVsAi = False Then
            AiVsAi = True
            AIVSAIBUtt.BackColor = Color.ForestGreen
            If PlayingAi = True Then
                PlayingAi = False
                AiButt.BackColor = Color.Red
            End If
        Else
            AiVsAi = False
            AIVSAIBUtt.BackColor = Color.Red
        End If
    End Sub
End Class
