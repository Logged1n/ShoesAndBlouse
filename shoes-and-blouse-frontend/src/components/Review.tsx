"use client"
import React, { useState } from 'react';
import axios from 'axios';

interface CreateReviewProps {
    productId: number;
    userId: number;
}

const CreateReview: React.FC<CreateReviewProps> = ({ productId, userId }) => {
    const [score, setScore] = useState<number>(1);
    const [title, setTitle] = useState<string>('');
    const [description, setDescription] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<boolean>(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);
        setSuccess(false);

        try {
            const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/v1/Review/Create`, {
                score,
                title,
                description,
                productId,
                userId,
            });
            if (response.status === 200) {
                setSuccess(true);
                setTitle('');
                setDescription('');
                setScore(1);
            }
        } catch (err) {
            setError('Błąd przy tworzeniu opinii');
        }
    };

    return (
        <div>
            <h1>Dodaj opinię</h1>
            {error && <div>{error}</div>}
            {success && <div>Opinia została dodana pomyślnie</div>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Ocena</label>
                    <select value={score} onChange={(e) => setScore(Number(e.target.value))}>
                        <option value={1}>1</option>
                        <option value={2}>2</option>
                        <option value={3}>3</option>
                        <option value={4}>4</option>
                        <option value={5}>5</option>
                    </select>
                </div>
                <div>
                    <label>Tytuł</label>
                    <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
                </div>
                <div>
                    <label>Opis</label>
                    <textarea value={description} onChange={(e) => setDescription(e.target.value)} />
                </div>
                <button type="submit">Dodaj opinię</button>
            </form>
        </div>
    );
};

export default CreateReview;
