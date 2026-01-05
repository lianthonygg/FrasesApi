using FrasesApi.Features.Frases.Domain.Entities;
using FrasesApi.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Features.Frases.Infrastructure.Persistence.Seed;

public class FrasesSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if(await context.Frases.AnyAsync())
            return;

        var frases = new List<Frase>
        {
            new()
            {
                Description = "Antes de juzgar a una persona, camina 3 lunas con sus mocasines",
                Author = "Proverbio Sioux"
            },
            new()
            {
                Description = "Lo que importa no es lo que te ocurre, sino cómo reaccionas ante el hecho",
                Author = "Epícteto"
            },
            new()
            {
                Description = "Conviértete en la persona con la que desearías estar",
                Author = "Rhonda Byrne, \"El Secreto\""
            },
            new() { Description = "Di tu verdad tranquila", Author = "Desiderata" },
            new()
            {
                Description = "La felicidad de tu vida depende de la calidad de tus pensamientos",
                Author = "Marco Aurelio"
            },

            new() { Description = "Observa al pensador", Author = "Eckart Tolle, \"El poder del ahora\"" },
            new()
            {
                Description = "Escucha la voz de tu cabeza sin juzgar", Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "¿Qué esta pasando dentro de mi en este momento?",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "¿Estoy relajado en este momento?", Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "¿Que está mal en este momento?", Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "Sientete agradecido por el momento presente",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "Sientete agradecido por la plenitud de la vida ahora mismo",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new() { Description = "Presta atención al presente", Author = "Eckart Tolle, \"El poder del ahora\"" },
            new()
            {
                Description =
                    "Presta atención a tu comportamiento, a tus reacciones, estados de ánimo, pensamientos, emociones, miedos y deseos, tal como surgen en el presente",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "Siente el Ser en lo profundo de tu pareja",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "Si algo te molesta sientete transparente",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "No te empeñes en demostrar que tú tienes razón y los demás están equivocados",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new()
            {
                Description = "La vida no es tan seria como la mente hace que parezca",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },
            new() { Description = "Esto también pasará", Author = "Eckart Tolle, \"El poder del ahora\"" },
            new()
            {
                Description =
                    "No reduzcas el presente a un medio para lograr un fin, un fin que esta proyectado por la mente en el futuro",
                Author = "Eckart Tolle, \"El poder del ahora\""
            },

            new()
            {
                Description =
                    "La tolerancia y la paciencia son mucho más profundas y efectivas que el odio y el rencor. Perdona, suelta y fluye.",
                Author = "Dalai Lama"
            },
            new() { Description = "Tu mejor maestro es este momento, escúchalo", Author = "Proverbio tibetano" },
            new() { Description = "Tu fe te ha salvado", Author = "Jesus de Nazaret" },

            new()
            {
                Description =
                    "Los que percibes como adversarios forman parte de tu paz, a la cual renuncias cuando los atacas",
                Author = "\"Un curso de milagros\", Helen Schucman"
            },
            new()
            {
                Description =
                    "Nunca te olvides de esto: en tus semejantes, o bien te encuentras a ti mismo o bien te pierdes a ti mismo",
                Author = "\"Un curso de milagros\", Helen Schucman"
            },
            new()
            {
                Description = "No busques dentro de ti; más bien observa a quien tienes delante de ti",
                Author = "\"Un curso de milagros\", Helen Schucman"
            },

            new()
            {
                Description =
                    "Cuando los seres humanos se toman demasiado en serio a sí mismos, se vuelven críticos y juzgan, tanto a ellos como a los demás",
                Author = "\"Las 36 leyes espirituales de la vida\", Diana Cooper"
            },
            new()
            {
                Description = "No te preocupes de lo que puedes obtener, preocúpate de lo que puedes aportar",
                Author = "\"Este no es el evangelio que quise ofrecerte\", Enric Corberá"
            },

            new() { Description = "¿Qué haría el amor?", Author = "\"Conversaciones con Dios\", Neale Donald" },
            new()
            {
                Description = "Tu vida se desarrolla según tus intenciones sobre ella. ¿Cuál es tu intención ahora?",
                Author = "\"Conversaciones con Dios\", Neale Donald"
            },
            new()
            {
                Description = "La vida no tiene nada de espantoso si no te preocupas por los resultados",
                Author = "\"Conversaciones con Dios\", Neale Donald"
            },
            new()
            {
                Description = "Aquello a lo que te resistas persistirá; aquello que mires, desaparecerá",
                Author = "\"Conversaciones con Dios\", Neale Donald"
            },
            new()
            {
                Description =
                    "Vivir la vida sin expectativas es la libertad, no preocuparse por obtener unos resultados determinados",
                Author = "\"Conversaciones con Dios\", Neale Donald"
            },
            new() { Description = "¿Cómo puedo ser útil?", Author = "\"Conversaciones con Dios\", Neale Donald" },

            new()
            {
                Description = "Emplea tus sentidos plenamente",
                Author = "\"Practicando el poder del ahora\", Eckart Tolle"
            },

            new()
            {
                Description = "Imponte tareas en apariencia imposibles", Author = "\"Tus zonas sagradas\", Wayne Dier"
            },
            new()
            {
                Description = "Fíjate en los actos de bondad del resto de personas más que en sus malas acciones",
                Author = "\"Tus zonas sagradas\", Wayne Dyer"
            },
            new() { Description = "Tal y como ha sido tu fe, así suceda contigo", Author = "Jesús de Nazaret" },

            new() { Description = "Nos convertimos en lo que pensamos", Author = "Buda" },
            new()
            {
                Description =
                    "Estás hoy donde tus pensamientos te han traido; mañana estarás donde tus pensamientos te lleven",
                Author = "James Allen"
            },

            new() { Description = "¿Qué hara el amor ahora si no hubiera miedos?", Author = "Neale Donald Walsch" }
        };
        
        await context.Frases.AddRangeAsync(frases);
        await context.SaveChangesAsync();
    }
}