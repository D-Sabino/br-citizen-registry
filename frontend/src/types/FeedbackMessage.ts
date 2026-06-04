export interface FeedbackMessage {
    type: 'success' | 'warning' | 'error';
    title: string;
    description: string;
    details?: string | string[];
}