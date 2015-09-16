''' <summary>
''' Manages the wave spawns.
''' </summary>
''' <remarks></remarks>
Public Class WaveManager
    Private Waves As List(Of Wave)
    Private Data As WaveData
    Public Sub New()
        Waves = Data.Waves
    End Sub
    Private Sub LoadWaves()
        'Initialize waves here

    End Sub
    Public ReadOnly Property GetWaves As List(Of Wave)
        Get
            Return Waves
        End Get
    End Property
End Class
