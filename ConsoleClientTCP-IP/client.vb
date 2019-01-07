'перепечатано с https://metanit.com/sharp/net/3.2.php
Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module client

	Sub Main()
		Dim port As Integer = 8005
		Dim address As String = "127.0.0.1"
		Try
			Dim ipPoint As New IPEndPoint(IPAddress.Parse(address), port)
			Dim socket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
			socket.Connect(ipPoint)
			Console.Write("Введите сообщение:")
			Dim message As String = Console.ReadLine()
			Dim data() As Byte = Encoding.Unicode.GetBytes(message)
			socket.Send(data)



			'получаем ответ
			ReDim data(256)
			Dim builder As New StringBuilder
			Dim bytes As Integer = 0

			Do
				bytes = socket.Receive(data, data.Length, 0)
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes))
			Loop While (Socket.Available > 0)
			Console.WriteLine("ответ сервера: " & builder.ToString())

			' закрываем сокет
			socket.Shutdown(SocketShutdown.Both)
			socket.Close()
		Catch ex As Exception
			Console.WriteLine(ex.Message)
		End Try
		Console.Read()

	End Sub

End Module
