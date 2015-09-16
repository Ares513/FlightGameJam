''' <summary>
''' Basic class that defines only the position, velocity, etc of an entity.
''' </summary>
''' <remarks></remarks>
Public Class SimpleEntity
        Private position As Vector2
        Private mySize As Size
        Public Property Size As Size
            Get
                Return mySize
            End Get
            Set(value As Size)
                mySize = value
            End Set
        End Property
        Public Property Pos As Vector2
            Get
                Return position
            End Get
            Set(value As Vector2)
                position = value
            End Set
        End Property
        Private velocity As Vector2
        Public Property Vel As Vector2
            Get
                Return velocity
            End Get
            Set(value As Vector2)
                velocity = value
            End Set
        End Property
        Public ReadOnly Property GetRectangle As Rectangle
            Get
                Return New Rectangle(CInt(Pos.X), CInt(Pos.Y), Size.Width, Size.Height)
            End Get

        End Property

End Class
