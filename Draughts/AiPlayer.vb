Public Class AiPlayer
    Public Colour As Integer '1 white -1 black
    Public timecounter As Integer
    Public Sub New(ByVal col As Integer)
        Me.Colour = col

    End Sub
    Public Sub timer_tick()
        timecounter += 1
    End Sub
    Public Function StartNegaMax(ByVal board(,) As Piece, ByVal whosturn As Integer, ByVal depth As Integer, ByVal WhosMoveOG As Integer)
        Dim Allmove As New List(Of Form1.Move)
        Dim score As Integer
        Dim BestScore As Integer = -999999
        Dim BestMove As Form1.Move
        Dim Time As New Timers.Timer
        AddHandler Time.Elapsed, AddressOf timer_tick
        Time.Interval = 100
        timecounter = 0
        Time.Enabled = True
        Dim FinalBestMove As Form1.Move
        Dim depthcounter As Integer = 1
        Allmove = Form1.GenerateAllMovesAI(whosturn, Allmove, depth, False)
        Form1.PieceCount()
        Allmove = ReorderMoves(Allmove)

        Do Until (timecounter / 10 > 5 Or depthcounter > 100)
            BestScore = -9999999
            For Each M In Allmove
                If timecounter / 10 > 5 Then


                    Exit For
                End If
                Form1.MakeMove(M)
                score = -Negamax(board, -whosturn, depthcounter, WhosMoveOG, -999999, 999999)
                Form1.UnmakeMove(M)
                M.score = score
                If score > BestScore And score <> 0 Then
                    BestScore = score
                    BestMove = M
                End If
            Next
            FinalBestMove = BestMove
            depthcounter += 1
            Allmove = ReorderMovesID(Allmove)
        Loop
        Time.Enabled = False
        Form1.CountLabel.Text = "Count: " & Form1.NegaMaxCount & vbCrLf & "Score: " & BestScore & vbCrLf & "Depth: " & depthcounter & vbCrLf & "Time: " & timecounter / 10
        Allmove = Nothing
        Form1.BestMoveScore = BestScore

        Return FinalBestMove

    End Function
    Public Function ReorderMovesID(ByVal all_moves As List(Of Form1.Move))
        Dim reordered As New List(Of Form1.Move)
        Dim count As Integer
        Dim counter As Integer = 0
        Dim isokay As Boolean = False
        Dim pos As Form1.position
        For Each Move In all_moves

            count = 0
            If counter = 0 Then
                reordered.Add(Move)
            Else
                isokay = False
                For Each FMOVE In reordered
                    If FMOVE.score <= Move.score Then
                        reordered.Insert(count, Move)
                        isokay = True
                        Exit For

                    End If
                    count += 1
                Next
                If isokay = False Then
                    reordered.Add(Move)
                End If
            End If

            counter += 1
        Next
        all_moves = reordered
        reordered = Nothing
        'MsgBox(reordered.Count & "   " & all_moves.Count)
        Return all_moves

    End Function

    Private Function Negamax(ByVal board(,) As Piece, ByVal whosturn As Integer, ByVal depth As Integer, ByVal WhosMoveOG As Integer, ByVal ALPHA As Integer, ByVal BETA As Integer)
        Dim Allmove As New List(Of Form1.Move)
        Dim score As Integer
        If timecounter / 10 > 5 Then

            Return ALPHA
        End If
        If depth = 0 Then

            Return NegamaxTakesOnly(board, -whosturn, depth, WhosMoveOG, -BETA, -ALPHA)
        End If
        Allmove = Form1.GenerateAllMovesAI(whosturn, Allmove, depth, False)
        If Allmove.Count = 0 Then
            If whosturn = WhosMoveOG Then
                Return 99999
            Else
                Return -99999
            End If

        End If
        Allmove = ReorderMoves(Allmove)
        Form1.PieceCount()
        If Form1.WhiteCount = 0 Then
            If whosturn = -1 Then
                Return 99999
            Else
                Return -99999
            End If
        ElseIf Form1.BlackCount = 0 Then
            If whosturn = 1 Then
                Return 99999
            Else
                Return -99999
            End If
        End If
        For Each M In Allmove
            If timecounter / 10 > 5 Then

                Return ALPHA
            End If
            Form1.MakeMove(M)

            score = -Negamax(board, -whosturn, depth - 1, WhosMoveOG, -BETA, -ALPHA)
            Form1.UnmakeMove(M)
            If score >= BETA Then
                Allmove = Nothing

                Return BETA
            End If

            If score > ALPHA Then ALPHA = score
        Next
        Allmove = Nothing
        Return ALPHA
    End Function
    Private Function NegamaxTakesOnly(ByVal board(,) As Piece, ByVal whosturn As Integer, ByVal depth As Integer, ByVal WhosMoveOG As Integer, ByVal ALPHA As Integer, ByVal BETA As Integer)
        Dim Allmove As New List(Of Form1.Move)
        Dim score As Integer
      
        
        Allmove = Form1.GenerateAllMovesAI(whosturn, Allmove, 0, True)
        If Allmove.Count = 0 Then
            
            If Form1.WhiteCount = 0 Then
                If whosturn = -1 Then
                    Return 99999
                Else
                    Return -99999
                End If
            ElseIf Form1.BlackCount = 0 Then
                If whosturn = 1 Then
                    Return 99999
                Else
                    Return -99999
                End If
            End If
            Form1.NegaMaxCount += 1
            Return EvaluateBoard(board, whosturn, WhosMoveOG)
        End If
        Allmove = ReorderMoves(Allmove)
        Form1.PieceCount()
       
        For Each M In Allmove
            If timecounter / 10 > 5 Then

                Return ALPHA
            End If
            Form1.MakeMove(M)

            score = -NegamaxTakesOnly(board, -whosturn, depth - 1, WhosMoveOG, -BETA, -ALPHA)
            Form1.UnmakeMove(M)
            If score >= BETA Then
                Allmove = Nothing

                Return BETA
            End If

            If score > ALPHA Then ALPHA = score
        Next
        Allmove = Nothing
        Return ALPHA
    End Function

    Public Function ReorderMoves(ByVal all_moves As List(Of Form1.Move))
        Dim reordered As New List(Of Form1.Move)
        Dim count As Integer
        Dim counter As Integer = 0
        Dim isokay As Boolean = False
        Dim pos As Form1.position
        For Each Move In all_moves

            If Move.Origin.Size = 1 Then
                If Move.Origin.colour = -1 Then
                    Move.score += (Move.Target.pos.Y * 10)
                Else
                    Move.score += ((7 - Move.Target.pos.Y) * 10)
                End If
            End If
            If Move.istake = True Then
                Move.score += 100
            End If




            count = 0
            If counter = 0 Then
                reordered.Add(Move)
            Else
                isokay = False
                For Each FMOVE In reordered
                    If FMOVE.score <= Move.score Then
                        reordered.Insert(count, Move)
                        isokay = True
                        Exit For

                    End If
                    count += 1
                Next
                If isokay = False Then
                    reordered.Add(Move)
                End If
            End If

            counter += 1
        Next
        all_moves = reordered
        reordered = Nothing
        'MsgBox(reordered.Count & "   " & all_moves.Count)
        Return all_moves

    End Function
    Public Function EvaluateBoard(ByVal board(,) As Piece, ByVal whosturn As Integer, ByVal WhosMoveOG As Integer)
        Dim score As Integer
        Dim perspective As Integer
        Dim MaterialScore As Integer
        Dim UpBoardScore As Integer

        MaterialScore = (Form1.WhiteCount - Form1.BlackCount) * 300
        If whosturn = WhosMoveOG Then perspective = 1 Else perspective = -1
        Select Case whosturn
            Case -1
                For Each P In Form1.ListOfblackPieces



                    If board(P.X, P.Y).GetSize = 1 Then
                        If whosturn = -1 Then
                            UpBoardScore += (P.Y * 10)
                        Else
                            UpBoardScore += ((7 - P.Y) * 10)
                        End If
                    End If


                Next
            Case 1
                For Each P In Form1.ListOfWhitePieces



                    If board(P.X, P.Y).GetSize = 1 Then
                        If whosturn = -1 Then
                            UpBoardScore += (P.Y * 10)
                        Else
                            UpBoardScore += ((7 - P.Y) * 10)
                        End If
                    End If


                Next
        End Select


        Return (MaterialScore + UpBoardScore) * perspective
    End Function


End Class
