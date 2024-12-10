import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'http://localhost:5137/', // Замените на URL вашего API
    headers: {
        'Content-Type': 'application/json',
    },
});

export default apiClient;
