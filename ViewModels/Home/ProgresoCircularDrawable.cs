
namespace ViewModels.Home;
using Microsoft.Maui.Graphics;
using System;
public class ProgresoCircularDrawable : IDrawable
{
    public float Progreso { get; set; } = 0f; // 0.0 - 1.0
    public string TiempoRestante { get; set; } = "00:00";

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        float centroX = dirtyRect.Width / 2;
        float centroY = dirtyRect.Height / 2;

        float radioExterno = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 - 10;
        float radioInterno = radioExterno - 15;

        // 1. Dibujar círculo exterior
        canvas.StrokeColor = Colors.LightGray;
        canvas.StrokeSize = 10;
        canvas.DrawCircle(centroX, centroY, radioExterno);

        // 2. Dibujar círculo negro completo (base)
        canvas.FillColor = Colors.Black;
        canvas.FillCircle(centroX, centroY, radioInterno);

        // 3. Dibujar "sector consumido" encima en color blanco (para ocultar)
        if (Progreso > 0)
        {
            canvas.FillColor = Colors.White;

            var path = new PathF();
            float startAngle = -90; // Empezamos arriba (270° = -90°)
            float sweepAngle = 360 * Progreso;

            // Convertimos a radianes
            float startRadians = DegreesToRadians(startAngle);
            path.MoveTo(centroX, centroY); // Punto central

            int segmentos = 100; // más segmentos = curva más suave
            for (int i = 0; i <= segmentos; i++)
            {
                float t = (float)i / segmentos;
                float angle = startAngle + sweepAngle * t;
                float radians = DegreesToRadians(angle);

                float x = centroX + (radioInterno + 1) * (float)Math.Cos(radians);
                float y = centroY + (radioInterno + 1) * (float)Math.Sin(radians);
                path.LineTo(x, y);
            }

            path.Close();
            canvas.FillPath(path);
        }

        // 4. Dibujar fondo rojo detrás del texto
        float anchoTexto = 100;
        float altoTexto = 40;
        float posX = centroX - (anchoTexto / 2);
        float posY = centroY - (altoTexto / 2);

        // Fondo rojo
        canvas.FillColor = Colors.Black;
        canvas.FillCircle(centroX, centroY, radioInterno * 0.3f);

        // Texto blanco centrado encima
        string textoTiempo = TiempoRestante;

        canvas.FontColor = Colors.White;
        canvas.FontSize = 24;

        canvas.DrawString(
            textoTiempo,
            x: posX,
            y: posY,
            width: anchoTexto,
            height: altoTexto,
            HorizontalAlignment.Center,
            VerticalAlignment.Center,
            TextFlow.OverflowBounds
        );
    }

    private float DegreesToRadians(float degrees)
    {
        return (float)(Math.PI / 180) * degrees;
    }
}