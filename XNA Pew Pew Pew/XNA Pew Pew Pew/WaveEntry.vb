Public Class WaveEntry
    Public EnemyID As String 'The name of the mob to spawn. MobData will be pulled from the MobLibrary. If the data is wrong, the default mob will be spawned.
    Public WaveTime As Single 'The time, in milliseconds, during the wave, that the mob spawns at.
    Public WaveY As Integer 'Where, from top to bottom on the screen, does the mob spawn?

End Class
