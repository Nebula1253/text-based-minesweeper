Module Module1

    Sub Main()
        Dim MineField(8, 8), PlayField(8, 8) As Char
        SetBombs(MineField)
        SetNumbers(MineField)
        SetPlayfield(PlayField)

        Dim InputRows, InputColumn As Integer
        Dim OptionChar As Char
        Dim Won As Boolean = False
        Dim Bomb As Boolean = False
        Console.WriteLine("Welcome to MineSweeper: Terrible Edition!")
        Do Until Won = True Or Bomb = True
            DisplayPField(PlayField)
            Console.WriteLine("What would you like to do?")
            Console.WriteLine("U - Uncover tile")
            Console.WriteLine("F - Set Flag")
            Console.WriteLine("P - Set possible flag")
            Console.WriteLine("R - Remove flag")
            OptionChar = Console.ReadLine()
            Console.WriteLine()
            Console.WriteLine("Input the row and column")
            InputRows = Console.ReadLine
            InputColumn = Console.ReadLine

            Select Case OptionChar
                Case "U"
                    If PlayField(InputRows, InputColumn) = "*" Then
                        UncoverTile(InputRows, InputColumn, Bomb, PlayField, MineField)
                    Else : Console.WriteLine("This cannot be uncovered")
                    End If
                Case "F"
                    If PlayField(InputRows, InputColumn) = "*" Then
                        PlayField(InputRows, InputColumn) = "F"
                    Else : Console.WriteLine("This cannot be flagged")
                    End If
                Case "R"
                    If PlayField(InputRows, InputColumn) = "F" Then
                        PlayField(InputRows, InputColumn) = "*"
                    Else : Console.WriteLine("This cannot be unflagged")
                    End If
                Case "P"
                    If PlayField(InputRows, InputColumn) = "*" Then
                        PlayField(InputRows, InputColumn) = "?"
                    Else : Console.WriteLine("This cannot be flagged")
                    End If
            End Select

            If Bomb = True Then
                DisplayPField(PlayField)
                Console.WriteLine("Better luck next time!")
            End If

            Won = CheckIfWon(PlayField)
            If Won = True Then
                Console.WriteLine("Congratulations! You found all the mines!")
            End If
        Loop
        Console.ReadLine()
    End Sub

    Sub SetBombs(ByRef MineField(,) As Char)
        Dim i, j, BombCount, random As Integer
        Dim rnd As New Random
        Do Until BombCount = 10
            For i = 0 To 8
                For j = 0 To 8
                    random = rnd.Next(1, 6)
                    If MineField(i, j) <> "x" Then
                        If random = 2 And BombCount < 10 Then
                            MineField(i, j) = "x"
                            BombCount = BombCount + 1
                        Else : MineField(i, j) = "_"
                        End If
                    End If
                Next
            Next
        Loop
    End Sub

    Sub SetNumbers(ByRef MineField(,) As Char)
        Dim i, j, k, l, SurrCount As Integer
        SurrCount = 0
        For i = 0 To 8
            For j = 0 To 8
                If MineField(i, j) <> "x" Then
                    For k = (i - 1) To (i + 1)
                        For l = (j - 1) To (j + 1)
                            If k >= 0 And l >= 0 And k <= 8 And l <= 8 Then
                                If MineField(k, l) = "x" Then
                                    SurrCount = SurrCount + 1
                                End If
                            End If
                        Next
                    Next
                    If SurrCount > 0 Then
                        MineField(i, j) = SurrCount.ToString
                    End If
                    SurrCount = 0
                End If
            Next
        Next
    End Sub

    Sub SetPlayfield(ByRef Playfield(,) As Char)
        Dim i, j As Integer
        For i = 0 To 8
            For j = 0 To 8
                Playfield(i, j) = "*"
            Next
        Next
    End Sub

    Sub DisplayPField(ByRef Playfield(,) As Char)
        Dim i, j As Integer
        Console.WriteLine("   0 1 2 3 4 5 6 7 8")
        Console.WriteLine()
        For i = 0 To 8
            Console.Write(i.ToString + "  ")
            For j = 0 To 8
                Console.Write(Playfield(i, j) + " ")
            Next
            Console.WriteLine()
        Next
    End Sub

    Sub UncoverTile(ByVal row As Integer, ByVal column As Integer, ByRef bomb As Boolean, ByRef playfield(,) As Char, ByRef minefield(,) As Char)
        playfield(row, column) = minefield(row, column)
        Dim m, n As Integer
        If playfield(row, column) = "x" Then
            bomb = True
        ElseIf playfield(row, column) = "_" Then
            For m = row - 1 To row + 1
                For n = column - 1 To column + 1
                    If m >= 0 And m <= 8 And n >= 0 And n <= 8 Then
                        If playfield(m, n) = "*" Then
                            UncoverTile(m, n, False, playfield, minefield)
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Function CheckIfWon(ByRef Playfield(,) As Char) As Boolean
        Dim i, j, ClearSquares As Integer
        ClearSquares = 0
        For i = 0 To 8
            For j = 0 To 8
                If Playfield(i, j) <> "*" Then
                    ClearSquares = ClearSquares + 1
                End If
            Next
        Next
        If ClearSquares = 81 Then
            Return True
        Else : Return False
        End If
    End Function
End Module
