import React from 'react';
import UserProfile from '@/components/UserProfile';

const App: React.FC = () => {
    // Zakładam, że masz sposób na uzyskanie userId, np. z parametrów URL lub stanu aplikacji
    const userId = '2'; // Zastąp odpowiednim ID użytkownika

    return (
        <div>
            <h1>Profil użytkownika</h1>
            <UserProfile userId={userId} />
        </div>
    );
};

export default App;
