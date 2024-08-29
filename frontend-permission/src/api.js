import axios from 'axios';
import { toast } from 'sonner';


const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});


api.interceptors.response.use(
    (response) => response,
    (error) => {
        console.log(error)
        if (error.response) {
            if (error.response.data) {
                toast.error(error.response.data.message);
            } else {
                toast.error('Error desconocido del servidor.');
            }
        } else if (error.request) {
            if (!navigator.onLine) {
                toast.error('No hay conexión a Internet. Por favor, verifica tu conexión.');
            } else {
                toast.error('No se pudo conectar al servidor. Intenta nuevamente más tarde.');
            }
        } else {
            toast.error('Error al configurar la solicitud: ' + error.message);
        }

        return Promise.reject(error);
    }
);

export default api
