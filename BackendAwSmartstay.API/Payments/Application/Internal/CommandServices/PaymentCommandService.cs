using BackendAwSmartstay.API.Bookings.Domain.Repositories; // <--- IMPORTANTE: Acceso a Reservas
using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Payments.Domain.Model.Commands;
using BackendAwSmartstay.API.Payments.Domain.Repositories;
using BackendAwSmartstay.API.Payments.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Payments.Application.Internal.CommandServices;

/// <summary>
/// Implementation of the payment command service.
/// Orchestrates the payment process and updates the booking status upon success.
/// </summary>
public class PaymentCommandService(
    IPaymentRepository paymentRepository,
    IBookingRepository bookingRepository, // <--- Inyectamos el repositorio de reservas
    IUnitOfWork unitOfWork) 
    : IPaymentCommandService
{
    public async Task<Payment?> Handle(ProcessPaymentCommand command)
    {
        // 1. Iniciar el proceso de pago (Estado Pendiente)
        var payment = new Payment(command);

        // 2. Lógica de Simulación (Gateway Fake)
        bool isApproved = true;

        // Reglas de negocio simuladas
        if (command.Amount <= 0) isApproved = false;
        if (command.CardNumber.EndsWith("0000")) isApproved = false; // Simular tarjeta rechazada

        if (isApproved)
        {
            payment.Complete(); // Cambia estado de pago a Completed (1)

            // --- JUGADA CLAVE: ACTUALIZAR LA RESERVA ---
            var booking = await bookingRepository.FindByIdAsync(command.BookingId);
            if (booking != null)
            {
                booking.Confirm(); // Cambia estado de reserva a Confirmed
                bookingRepository.Update(booking);
                Console.WriteLine($"Booking #{booking.Id} has been confirmed via Payment.");
            }
            else 
            {
                // Si no existe la reserva, no deberíamos cobrar
                throw new Exception("Booking not found provided for payment.");
            }
            // -------------------------------------------
        }
        else
        {
            payment.Fail(); // Cambia estado de pago a Failed (2)
            Console.WriteLine("Payment declined by simulation logic.");
        }

        // 3. Guardar TODO en una sola transacción (Unit of Work)
        // Esto guarda el Pago y la actualización de la Reserva al mismo tiempo.
        await paymentRepository.AddAsync(payment);
        await unitOfWork.CompleteAsync();

        return payment;
    }
}