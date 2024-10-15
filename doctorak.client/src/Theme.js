import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    palette: {
        primary: {
            main: '#124963', // Black for primary
        },
        secondary: {
            main: '#fff' // white for secondary
        },
        reddish: {
            main: "#d32f2f"
        },
        idk: {
            main: '#28698a'
        },
        background: {
            default: '#f7fcfc', // White background
        },
        text: {
            primary: '#000', // Black text
            secondary: '#4a7766', // Greyish secondary text
        },
    },

    typography: {
        fontFamily: 'Roboto, Arial, sans-serif, Grey Qo',
        h1: {
            fontSize: '3rem',
            fontWeight: 700,
            letterSpacing: '0.1rem',
            color: '#000',
        },
        h2: {
            fontSize: '2rem',
            fontWeight: 600,
            letterSpacing: '0.05rem',
            color: '#000',
        },
        button: {
            textTransform: 'none',
        },
    },
});

export default theme;