import axios from "axios";

const axiosInstance = axios.Create({
    baseUrl: "https://locahost:7244/api",
});

//request intercptor injecting token into header
axiosInstance.interceptors.request.use((config) => {
    const token = localStorage.getItem("Token");  //fetch token

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});