Public Class Piece

    Protected Colour As Integer ' 1 for white -1 to black 0 for blank
    Protected Pos As Point
    Protected HasMoved As Boolean
    Protected VisRep As PictureBox
    Protected SquareINT As Integer ' 1 Light, -1 Dark
    Protected Moves As List(Of Form1.Move)
    Protected Size As Integer
    Private Sub Piece_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'MsgBox(Pos.X & " " & Pos.Y)

        Dim IsOkay As Boolean = False
        If Form1.AlreadySelected = True Then
            Form1.ClearDisplayedMoves()
        End If
        Dim TempMove As Form1.Move
        Form1.AlreadySelected = True
        If Me.Colour = 0 Then

            For Each Smove In Form1.DisplayedMoves
                If Smove.Target.pos = Me.Pos Then
                    TempMove = Smove
                    IsOkay = True
                End If
            Next
        Else
            Form1.SelectedPiece = Me.Pos
            Form1.DisplayedMoves = Me.Moves
            Form1.DrawDisplayedMoves(Me.Moves)
        End If
        If IsOkay = True Then
            Form1.AlreadySelected = False
            Form1.MakeMove(TempMove)
            Form1.Session()

        End If


    End Sub
    Private Sub Double_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox("Colour: " & Me.Colour & vbCrLf & "Size: " & Me.Size)
    End Sub

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal SquareID As Integer, ByVal Col As Integer)
        Me.VisRep = New PictureBox
        Me.Size = 0
        AddHandler Me.VisRep.Click, AddressOf Piece_click
        AddHandler Me.VisRep.DoubleClick, AddressOf Double_Click
        Me.VisRep.SizeMode = PictureBoxSizeMode.StretchImage
        Me.Pos.X = x
        Me.Pos.Y = y

        Me.Moves = New List(Of Form1.Move)

        Me.HasMoved = False
        Me.Colour = Col

        Me.SquareINT = SquareID
        Me.SetImage()
        Me.VisRep.SetBounds((x * 75) + 25, (y * 75) + 25, 75, 75)

        Me.DrawPiece()
    End Sub
    Public Sub ClearMoves()
        Me.Moves.Clear()
    End Sub

    Public Sub SetDisplayedSingleMove()
        Me.VisRep.Image = Image.FromFile(Form1.assetsPATH & "SelectedDarkSquare.png")
    End Sub
    Public Function HasMove() As Boolean
        Return Me.HasMove
    End Function
    Public Sub HasMove(ByVal TF As Boolean)
        Me.HasMoved = TF
    End Sub
   
    Public Sub SetSquare(ByVal SID As Integer)
        Select Case SID

        End Select
    End Sub




    Public Sub addmoves(ByVal x As Integer, ByVal y As Integer, ByVal col As Integer, ByVal sizeof As Integer, ByVal TakeMove As Boolean, ByVal takePos As Point, ByVal TakeSize As Integer)
        Dim tempMove As Form1.Move
        tempMove.Origin.pos = Me.Pos
        tempMove.Origin.colour = Me.Colour
        tempMove.Origin.Size = Me.Size


        tempMove.istake = TakeMove
        tempMove.takepos = takePos
        tempMove.TakeSize = TakeSize


        tempMove.Target.pos.X = x
        tempMove.Target.pos.Y = y
        tempMove.Target.colour = col
        tempMove.Target.Size = sizeof


        Me.Moves.Add(tempMove)
    End Sub
    Public Sub addmoves(ByVal TempMove As Form1.Move)

        tempMove.Origin.pos = Me.Pos
        tempMove.Origin.colour = Me.Colour
        tempMove.Origin.Size = Me.Size
        Me.Moves.Add(tempMove)
    End Sub
    Public Function GenerateMoves() As List(Of Form1.Move)
        Dim YChange As Integer
        Dim TakePos As Point
        TakePos.X = 0
        TakePos.Y = 0
        Me.Moves.Clear()
        Select Case Me.Size
            Case 1, 0, -1
                If Me.Size = 0 Or -1 Then
                    Me.Size = 1
                End If

                If Me.Colour = 1 Then
                    YChange = -1
                    Try
                        TakePos.X = 0
                        TakePos.Y = 0
                        If Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = 0 Then
                            Me.addmoves(Me.Pos.X - 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                        ElseIf Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = -1 Then
                            If Form1.Board(Me.Pos.X - 2, Me.Pos.Y + -2).Colour = 0 Then
                                TakePos.X = Me.Pos.X - 1
                                TakePos.Y = Me.Pos.Y - 1
                                Me.addmoves(Me.Pos.X - 2, Me.Pos.Y + -2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X - 1, Me.Pos.Y + -1).GetColour)
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        TakePos.X = 0
                        TakePos.Y = 0
                        If Form1.Board(Me.Pos.X + 1, Me.Pos.Y + YChange).Colour = 0 Then
                            Me.addmoves(Me.Pos.X + 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                        ElseIf Form1.Board(Me.Pos.X + 1, Me.Pos.Y + YChange).Colour = -1 Then
                            If Form1.Board(Me.Pos.X + 2, Me.Pos.Y + -2).Colour = 0 Then
                                TakePos.X = Me.Pos.X + 1
                                TakePos.Y = Me.Pos.Y - 1
                                Me.addmoves(Me.Pos.X + 2, Me.Pos.Y + -2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X + 1, Me.Pos.Y + -1).GetColour)
                            End If
                        End If
                    Catch ex As Exception

                    End Try



                ElseIf Me.Colour = -1 Then
                    YChange = 1
                    Try
                        TakePos.X = 0
                        TakePos.Y = 0
                        If Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = 0 Then
                            Me.addmoves(Me.Pos.X - 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                        ElseIf Form1.Board(Me.Pos.X - 1, Me.Pos.Y + 1).Colour = 1 Then
                            If Form1.Board(Me.Pos.X - 2, Me.Pos.Y + 2).Colour = 0 Then
                                TakePos.X = Me.Pos.X - 1
                                TakePos.Y = Me.Pos.Y + 1
                                Me.addmoves(Me.Pos.X - 2, Me.Pos.Y + 2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X - 1, Me.Pos.Y + 1).GetColour)
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        TakePos.X = 0
                        TakePos.Y = 0
                        If Form1.Board(Me.Pos.X + 1, Me.Pos.Y + YChange).Colour = 0 Then
                            Me.addmoves(Me.Pos.X + 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                        ElseIf Form1.Board(Me.Pos.X + 1, Me.Pos.Y + 1).Colour = 1 Then
                            If Form1.Board(Me.Pos.X + 2, Me.Pos.Y + 2).Colour = 0 Then
                                TakePos.X = Me.Pos.X + 1
                                TakePos.Y = Me.Pos.Y + 1
                                Me.addmoves(Me.Pos.X + 2, Me.Pos.Y + 2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X + 1, Me.Pos.Y + 1).GetColour)
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                End If






            Case 2


                YChange = -1
                Try
                    TakePos.X = 0
                    TakePos.Y = 0
                    If Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = 0 Then
                        Me.addmoves(Me.Pos.X - 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                    ElseIf Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = -Me.Colour Then
                        If Form1.Board(Me.Pos.X - 2, Me.Pos.Y + -2).Colour = 0 Then

                            TakePos.X = Me.Pos.X - 1
                            TakePos.Y = Me.Pos.Y - 1
                            Me.addmoves(Me.Pos.X - 2, Me.Pos.Y + -2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X - 1, Me.Pos.Y + -1).GetColour)
                        End If
                    End If
                Catch ex As Exception

                End Try
                Try
                    TakePos.X = 0
                    TakePos.Y = 0
                    If Form1.Board(Me.Pos.X + 1, Me.Pos.Y + YChange).Colour = 0 Then
                        Me.addmoves(Me.Pos.X + 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                    ElseIf Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = -Me.Colour Then
                        If Form1.Board(Me.Pos.X + 2, Me.Pos.Y + -2).Colour = 0 Then

                            TakePos.X = Me.Pos.X - 1
                            TakePos.Y = Me.Pos.Y - 1
                            Me.addmoves(Me.Pos.X + 2, Me.Pos.Y + -2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X + 1, Me.Pos.Y + -1).GetColour)
                        End If
                    End If
                Catch ex As Exception

                End Try




                YChange = 1
                Try
                    TakePos.X = 0
                    TakePos.Y = 0
                    If Form1.Board(Me.Pos.X - 1, Me.Pos.Y + YChange).Colour = 0 Then
                        Me.addmoves(Me.Pos.X - 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                    ElseIf Form1.Board(Me.Pos.X - 1, Me.Pos.Y + 1).Colour = -Me.Colour Then
                        If Form1.Board(Me.Pos.X - 2, Me.Pos.Y + 2).Colour = 0 Then
                            TakePos.X = Me.Pos.X - 1
                            TakePos.Y = Me.Pos.Y + 1
                            Me.addmoves(Me.Pos.X - 2, Me.Pos.Y + 2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X - 1, Me.Pos.Y + 1).GetColour)
                        End If
                    End If
                Catch ex As Exception

                End Try
                Try
                    TakePos.X = 0
                    TakePos.Y = 0
                    If Form1.Board(Me.Pos.X + 1, Me.Pos.Y + YChange).Colour = 0 Then
                        Me.addmoves(Me.Pos.X + 1, Me.Pos.Y + YChange, 0, 0, False, TakePos, 0)
                    ElseIf Form1.Board(Me.Pos.X + 1, Me.Pos.Y + 1).Colour = -Me.Colour Then
                        If Form1.Board(Me.Pos.X + 2, Me.Pos.Y + 2).Colour = 0 Then
                            TakePos.X = Me.Pos.X + 1
                            TakePos.Y = Me.Pos.Y + 1
                            Me.addmoves(Me.Pos.X + 2, Me.Pos.Y + 2, 0, 0, True, TakePos, Form1.Board(Me.Pos.X + 1, Me.Pos.Y + 1).GetColour)
                        End If
                    End If
                Catch ex As Exception

                End Try


        End Select
    End Function
    Public Sub SetImage()
        Select Case Me.SquareINT
            Case 1
                Select Case Me.Colour
                    Case -1
                        Me.VisRep.Image = Form1.ImagesStore(5)
                    Case 0
                        Me.VisRep.Image = Form1.ImagesStore(0)
                    Case 1
                        Me.VisRep.Image = Form1.ImagesStore(4)

                End Select

            Case -1
                Select Case Me.Colour
                    Case -1
                        Me.VisRep.Image = Form1.ImagesStore(5)
                        Me.Size = 1
                    Case 0
                        Me.Size = 0
                        Me.VisRep.Image = Form1.ImagesStore(1)
                    Case 1
                        Me.Size = 1
                        Me.VisRep.Image = Form1.ImagesStore(4)
                End Select

        End Select
    End Sub
    Public Sub DrawPiece()
        Form1.Controls.Add(Me.VisRep)
    End Sub
    Public Function GetSize()
        Return Me.Size
    End Function
    Public Sub UpdateSquare(ByVal col As Integer, ByVal Ssize As Integer)
        Me.Colour = col
        Me.Size = Ssize
        If Form1.IsAICalculating = False Then
            Select Case col
                Case -1
                    Me.VisRep.Image = Form1.ImagesStore(5)
                Case 0
                    Me.VisRep.Image = Form1.ImagesStore(1)
                Case 1
                    Me.VisRep.Image = Form1.ImagesStore(4)
            End Select
        End If
        

    End Sub


    Public Function GetMoves() As List(Of Form1.Move)
        Return Me.Moves
    End Function




    Public Function GetColour()
        Return Me.Colour
    End Function
End Class
