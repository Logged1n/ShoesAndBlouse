"use client"
import React, { useEffect, useState } from 'react';
import axios from 'axios';

interface User {
    id: number;
    name: string;
    email: string;
    surname: string;
    // Dodaj inne właściwości użytkownika, które chcesz wyświetlić
}

const UserProfile: React.FC<{ userId: string }> = ({ userId }) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const { data } = await axios.get<User>(`/backendAPI/api/v1/User/GetById/${userId}`);
                setUser(data);
                setLoading(false);
            } catch (err) {
                setError('Błąd przy pobieraniu użytkownika');
                setLoading(false);
            }
        };

        fetchUser();
    }, [userId]);

    if (loading) {
        return <div>Ładowanie...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            {user ? (
                <div>
                    <h1>{user.name} {user.surname}</h1>
                    <p>Email: {user.email}</p>
                    {/* Dodaj inne właściwości do wyświetlenia */}
                </div>
            ) : (
                <div>Nie znaleziono użytkownika</div>
            )}
        </div>
    );
};

export default UserProfile;
