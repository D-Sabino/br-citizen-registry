import type { FeedbackMessage } from '../types/FeedbackMessage';

interface FeedbackDialogProps {
    message: FeedbackMessage | null;
    onClose: () => void;
}

const FeedbackDialog = ({ message, onClose }: FeedbackDialogProps) => {
    if (!message) {
        return null;
    }

    return (
        <div className="feedback-overlay">
            <div className={`feedback-card feedback-card--${message.type}`}>
                <button
                    type="button"
                    className="feedback-close"
                    onClick={onClose}
                    aria-label="Fechar mensagem"
                >
                    ×
                </button>

                <strong>{message.title}</strong>

                <p>{message.description}</p>

                {Array.isArray(message.details) && (
                    <div className="feedback-list">
                        {message.details.map((detail: string) => (
                            <p key={detail}>{detail}</p>
                        ))}
                    </div>
                )}

                {typeof message.details === 'string' && (
                    <p>{message.details}</p>
                )}
            </div>
        </div>
    );
};

export default FeedbackDialog;