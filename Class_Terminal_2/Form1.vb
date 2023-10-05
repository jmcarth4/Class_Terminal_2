





'Option Strict On
'Option Explicit On
Public Class Form1
        Dim portState As Boolean
        Public receiveByte(18) As Byte


        Private Sub Form1_UnLoad()
            Try
                SerialPort1.Close()

            Catch ex As Exception

            End Try
        End Sub


        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ListBox2.Items.Clear()
            ComboBox2.Items.Clear()
            ' portselect.items.clear()



            portState = False

            SerialPort1.BaudRate = 9600                    '9600 baud rate
            SerialPort1.DataBits = 8                       '8 data bits
            SerialPort1.StopBits = IO.Ports.StopBits.One   '1 stop bit
            SerialPort1.Parity = IO.Ports.Parity.None      'no Parity

            Timer1.Enabled = True                   'timer set to 50 ms

        End Sub


    Private Sub ComPortButton_Click(sender As Object, e As EventArgs)

        ComboBox2.Items.Clear()
        ListBox2.Items.Clear()

        For Each sp As String In My.Computer.Ports.SerialPortNames
            ListBox2.Items.Add(sp)
            ComboBox2.Items.Add(sp)
        Next

    End Sub

    Private Sub PortOpenButton_Click(sender As Object, e As EventArgs)
        If PortOpenButton.Text = "Connect" Then
            Try
                SerialPort1.Open()
                PortOpenButton.Text = "Disconnect"
                portState = True
            Catch ex As Exception
                MsgBox("Port Already Open or Port Unavailable")
                PortOpenButton.Text = "Connect"
                portState = False
            End Try
        Else
            Try
                SerialPort1.Close()
            Catch ex As Exception

            End Try
            portState = False
            PortOpenButton.Text = "Connect"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim dataIn As String
        dataIn = ""
        PortDataListBox.Items.Clear()
        PortDataListBox.Items.Add("Com port = " & SerialPort1.PortName) 'show current baud rate
        PortDataListBox.Items.Add("Baud Rate = " & SerialPort1.BaudRate) 'show current baud rate
        PortDataListBox.Items.Add("Data bits = " & SerialPort1.DataBits)
        PortDataListBox.Items.Add("Stop bits = " & SerialPort1.StopBits)
        PortDataListBox.Items.Add("Parity = " & SerialPort1.Parity)

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            SerialPort1.Close()

        Catch ex As Exception

        End Try

        PortOpenButton.Text = "Connect"
        portState = False
        Try
            SerialPort1.BaudRate = ListBox2.SelectedItem 'see if baud rate data is in the list box
        Catch ex As Exception
            SerialPort1.PortName = ListBox2.SelectedItem 'bot baud rate, save port name
        End Try
    End Sub


    Private Sub BaudRateButton_Click(sender As Object, e As EventArgs) Handles BaudRateButton.Click
        ListBox1.Items.Clear()                          'clear list box and load baud rate values
        ListBox1.Items.Add(1200)
        ListBox1.Items.Add(2400)
        ListBox1.Items.Add(4800)
        ListBox1.Items.Add(9600)
        ListBox1.Items.Add(19200)
        ListBox1.Items.Add(38400)
        ListBox1.Items.Add(57600)
        ListBox1.Items.Add(115200)
        ListBox1.Items.Add(230400)
        ListBox1.Items.Add(230400)
        ListBox1.Items.Add(460800)
        ListBox1.Items.Add(921600)

    End Sub

    Private Sub SendButton_Click(sender As Object, e As EventArgs)
        Dim dataOut As String

        dataOut = TextBox1.Text
        If portState = True Then
            If portState = True Then
                SerialPort1.Write(dataOut, 0, 1)
                ListBox1.Items.Add(dataOut)
            Else
                MsgBox("Please configure and open serial port to procede")

            End If
        End If

        Timer1.Enabled = False

        'Dim dataOut1 As String
        'Dim dataOut2 As String
        'Dim dataOut3 As String
        'Dim dataOut4 As String
        Dim dataIn1, dataIn2, dataIn3, dataIn4 As Integer



        If portState = True Then
            If dataOut IsNot "" Then
                SerialPort1.Write(dataOut, 0, 1)
                OutTermListBox.Items.Add(dataOut)

            Else
                Timer1.Enabled = True
                Exit Sub

            End If


            Try
                SerialPort1.Read(receiveByte, 0, 10)
                dataIn1 = receiveByte(0)
                dataIn2 = receiveByte(1)
                dataIn3 = receiveByte(2)
                dataIn4 = receiveByte(3)
                InTermListBox.Items.Add(Chr(dataIn1) & vbTab & Chr(dataIn2) & vbTab & Chr(dataIn3) & vbTab & Chr(dataIn4))
            Catch ex As Exception

            End Try

        Else
            MsgBox("Please configure and open serial port to procede")
            TextBox1.Text = " "
        End If
        Timer1.Enabled = True


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        TextBox1.Text = " "
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        OutTermListBox.Items.Clear()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs)
        InTermListBox.Items.Clear()
    End Sub

    Private Sub QuitButton_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class



