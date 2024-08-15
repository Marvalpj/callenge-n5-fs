import axios from 'axios';
import { toast } from 'sonner';


const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});


api.interceptors.response.use(
    (response) => response,
    (error) => {
        console.log(error)
        // Manejo de errores
        if (error.response) {
            // Si hay respuesta del servidor
            if (error.response.data) {
                toast.error(error.response.data.message);
            } else {
                toast.error('Error desconocido del servidor.');
            }
        } else if (error.request) {
            // Si no hay respuesta del servidor
            if (!navigator.onLine) {
                // Verifica si el navegador está en línea
                toast.error('No hay conexión a Internet. Por favor, verifica tu conexión.');
            } else {
                toast.error('No se pudo conectar al servidor. Intenta nuevamente más tarde.');
            }
        } else {
            // Algo pasó al configurar la solicitud
            toast.error('Error al configurar la solicitud: ' + error.message);
        }

        // Devuelve el error para que pueda ser manejado por el componente
        return Promise.reject(error);
    }
);

export default api
