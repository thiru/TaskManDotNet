import { useState, useEffect } from 'react';
import TaskItem from "./TaskItem";

// TODO: don't hard-code API domain here
const rootApiUri = 'http://localhost:8000/api/tasks';

export default function TaskItemList() {
  const [taskItems, setTaskItems] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(rootApiUri);
        if (!response.ok) {
          throw new Error('Error retrieving tasks');
        }
        const allTaskItems = await response.json();
        setTaskItems(allTaskItems);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleCreate = async (description) => {
    try {
      const response = await fetch(rootApiUri, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({description: description, isDone: false})
      });

      if (!response.ok) {
        throw new Error("Failed to delete task");
      }
      else {
        const newTaskItem = await response.json();
        setTaskItems(currTaskItems => [...currTaskItems, newTaskItem]);
      }
    } catch (error) {
      console.error("Create task failed:", error);
      alert('Failed to create a task');
    }
  };

  const handleEdit = async (updatedTaskItem) => {
    try {
      const response = await fetch(`${rootApiUri}/${updatedTaskItem.id}`, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(updatedTaskItem)
      });

      if (!response.ok) {
        throw new Error("Failed to edit task");
      }
      else {
        setTaskItems(currTaskItems => currTaskItems.map(x => {
          if (x.id === updatedTaskItem.id) {
            return {
              ...x,
              description: updatedTaskItem.description,
              isDone: updatedTaskItem.isDone};
          } else {
            return x;
          }
        }));
      }
    } catch (error) {
      console.error("Edit task failed:", error);
      alert('Failed to edit task');
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`${rootApiUri}/${id}`, {
        method: "DELETE"
      });

      if (!response.ok) {
        throw new Error("Failed to delete task");
      }
      else {
        setTaskItems(currTaskItems => currTaskItems.filter(x => x.id !== id));
      }
    } catch (error) {
      console.error("Delete task failed:", error);
      alert('Failed to delete task');
    }
  };

  return (
    <section className="block container">
      <button className="button mb-3"
              onClick={() => handleCreate(window.prompt('Task Description:'))}>
        Add Task
      </button>
      {
        loading ? (
          <div>
            <i className='fa-solid fa-spinner fa-spin mr-2' />
            <span>Loading your tasks...</span>
          </div>
        ) : (
            error ? (
              <div className='notification is-warning is-light'>
                {error}
              </div>
            ) : (
                <ul>
                  {taskItems.map(x => (
                    <li key={x.id}>
                      <TaskItem taskItem={x} onEdit={handleEdit} onDelete={handleDelete} />
                    </li>
                  ))}
                </ul>
              ))
      }
    </section>
  );
}
