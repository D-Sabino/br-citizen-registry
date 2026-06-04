import type { Citizen } from '../types/Citizen';

interface CreateCitizenRequest {
    fullName: string;
    cpf: string;
}

interface ApiErrorResponse {
    message?: string;
}

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const parseErrorMessage = async (response: Response): Promise<string> => {
    try {
        const data = await response.json() as ApiErrorResponse;

        if (data.message) {
            return data.message;
        }
    } catch {
        return 'Não foi possível processar a resposta da API.';
    }

    return 'Ocorreu um erro ao processar a solicitação.';
};

const handleResponse = async <T>(response: Response): Promise<T> => {
    if (response.ok) {
        return response.json() as Promise<T>;
    }

    const errorMessage = await parseErrorMessage(response);

    throw new Error(errorMessage);
};

export const citizenService = {
    create: async (request: CreateCitizenRequest): Promise<Citizen> => {
        const response = await fetch(`${API_BASE_URL}/Citizens`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(request)
        });

        return handleResponse<Citizen>(response);
    },

    search: async (term: string): Promise<Citizen> => {
        const queryParams = new URLSearchParams({
            term
        });

        const response = await fetch(`${API_BASE_URL}/Citizens/search?${queryParams.toString()}`);

        return handleResponse<Citizen>(response);
    }
};